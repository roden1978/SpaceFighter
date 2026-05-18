using System.Linq;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
public class Scene
{
    public event Action<BoxCollider2D> AddColliderComponent;
    public string Name;
    public Canvas Canvas => _canvas;
    public bool Active { get; set; } = false;
    public bool Started { get; private set; }

    private readonly List<GameObject> _gameObjects = [];
    private readonly Canvas _canvas;
    private readonly Camera _camera;

    public Scene(string name, Canvas canvas, Camera camera)
    {
        Name = name;
        _canvas = canvas;
        _camera = camera;
    }

    public void Initialize()
    {
        GameObject cameraGameObject = new("Camera");
        cameraGameObject.AddComponent(_camera);
        Register(cameraGameObject);
    }

    public void Register(IEnumerable<GameObject> gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
            Traverse(Add, gameObject);
    }

    public Scene Register(GameObject gameObject)
    {
        Traverse(Add, gameObject);

        return this;
    }
    private void Traverse(Action<GameObject> add, GameObject gameObject)
    {
        add(gameObject);
        foreach (Transform2D child in gameObject.Transform.Childrens)
            Traverse(Add, child.Gameobject);
    }
    private void Add(GameObject gameObject)
    {
        gameObject.Scene = this;

        if (gameObject.HasAnyIUIComponent())
        {
            if (gameObject.TryGetComponent(out CanvasHandler _) == false)
            {
                gameObject.AddComponent(new CanvasHandler());
            }
            _canvas.Register(gameObject);
            return;
        }

        if (gameObject.TryGetComponent(out BoxCollider2D collider2D))
        {
            AddColliderComponent?.Invoke(collider2D);
        }

        _gameObjects.Add(gameObject);

        if (Active)
            gameObject.Start();
    }
    public void Unregister(GameObject gameObject)
    {
        if (_gameObjects.Contains(gameObject))
        {
            gameObject.Scene = null;
            _gameObjects.Remove(gameObject);
        }

        if (false == _canvas.Contains(gameObject)) return;
        gameObject.Canvas = null;
        gameObject.Scene = null;
        _canvas.Remove(gameObject);
    }

    public void Update(GameTime gameTime)
    {
        if (false == Active) return;

        UpdateCanvas(gameTime);
        UpdateScene(gameTime);
    }

    public void Draw()
    {
        if (false == Active) return;
        if (_camera.CameraType == CameraTypes.Perspective)
        {
            // Reset for 3D drawing 
            _camera.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            _camera.GraphicsDevice.BlendState = BlendState.Opaque;
            _camera.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            DrawScene(_camera);
        }
        else
        {
            _camera.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, null, null, RasterizerState.CullClockwise, _camera.SpriteBatchEffect);
            DrawScene(_camera.SpriteBatch);
            _camera.SpriteBatch.End();
        }

        _camera.SpriteBatch.Begin();
        DrawCanvas(_camera.SpriteBatch);
        _camera.SpriteBatch.End();
    }

    private void UpdateScene(GameTime gameTime)
    {
        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i]?.Update(gameTime);
    }

    private void DrawScene(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i]?.Draw(spriteBatch);
    }
    private void DrawScene(Camera camera)
    {
        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i]?.Draw(camera);
    }
    private void UpdateCanvas(GameTime gameTime) =>
        _canvas.Update(gameTime);

    private void DrawCanvas(SpriteBatch spriteBatch) =>
        _canvas.Draw(spriteBatch);

    public void Start()
    {
        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i]?.Start();

        _canvas.Start();

        Active = true;
        SetStarted(true);
    }

    public GameObject FindGameObjectWithTag(Tags tag) =>
        _gameObjects.FirstOrDefault(x => x.Tag == tag);

    public IReadOnlyList<GameObject> FindGameObjectsWithComponent<T>() =>
        _gameObjects.Where(x => x.ContainsComponent<T>()).ToList();

    public IReadOnlyList<GameObject> FindAllGameObjects<T>() =>
        _gameObjects;


    public void CleanUp()
    {
        _canvas.CleanUp();

        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i]?.Destroy();

        _gameObjects.Clear();
    }

    public void SetActive(bool value)
    {
        _canvas.SetActive(value);

        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i]?.SetActive(value);

        Active = value;
    }
    public void SetStarted(bool value) =>
        Started = value;
}