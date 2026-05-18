using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Canvas
{
    public event Action UIObjectsCountHasChanged;
    public Rectangle CanvasRect { get; private set; }
    public string Name { get; private set; }
    public bool Active { get; private set; } = false;
    public bool Started { get; private set; }
    private readonly List<GameObject> _gameObjects = [];
    private readonly MouseEventSystem _mouseEventSystem;
    public Canvas(MouseEventSystem mouseEventSystem, string name, int width, int height)
        : this(mouseEventSystem, name, 0, 0, width, height)
    {
    }

    public Canvas(MouseEventSystem mouseEventSystem, string name, int x, int y, int width, int height)
    {
        CanvasRect = new Rectangle(x, y, width, height);
        Name = name;
        _mouseEventSystem = mouseEventSystem;
        _mouseEventSystem.PositionUpdate += OnPositionUpdate;
        _mouseEventSystem.ClickUpdate += OnClickUpdate;
    }

    public Canvas Register(GameObject gameObject)
    {
        Add(gameObject);
        return this;
    }

    public bool Contains(GameObject gameObject) =>
        _gameObjects.Contains(gameObject);

    public void Remove(GameObject gameObject)
    {
        _gameObjects.Remove(gameObject);
    }

    private void Add(GameObject gameObject)
    {
        gameObject.Canvas = this;
        _gameObjects.Add(gameObject);

        if (gameObject.TryGetComponent(out CanvasHandler canvasHandler) == false)
            throw new ArgumentNullException($"UI game object {gameObject.Name} must have CanvasHandler component!");


        if (gameObject.HasAnyIUIComponent())
        {
            IEnumerable<ICanvasDrawableComponent> components = gameObject.GetDraws();

            foreach (ICanvasDrawableComponent component in components)
            {
                canvasHandler.Width = component.Sprite.Width;
                canvasHandler.Height = component.Sprite.Height;
            }
        }

        if (gameObject.TryGetComponent(out IInteractable interactable))
            canvasHandler.InteractableComponent = interactable;

        if (Active)
            gameObject.Start();

        UIObjectsCountHasChanged?.Invoke();
    }
    private void OnClickUpdate(object sender, MouseEventArgs e)
    {
        for (int i = 0; i < _gameObjects.Count; i++)
            if (_gameObjects[i].Active & _gameObjects[i].TryGetComponent(out CanvasHandler canvasHandler))
                canvasHandler.OnClickUpdate(sender, e);
    }
    private void OnPositionUpdate(object sender, MouseEventArgs e)
    {
        for (int i = 0; i < _gameObjects.Count; i++)
            if (_gameObjects[i].Active & _gameObjects[i].TryGetComponent(out CanvasHandler canvasHandler))
                canvasHandler.OnPositionUpdate(sender, e);
    }
    public void Update(GameTime gameTime)
    {
        if (false == Active) return;

        _mouseEventSystem.Update(gameTime);

        for (int i = 0; i < _gameObjects.Count; i++)
            if (_gameObjects[i].Active)
                _gameObjects[i].Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (false == Active) return;

        for (int i = 0; i < _gameObjects.Count; i++)
            if (_gameObjects[i].Active)
                _gameObjects[i].Draw(spriteBatch);
    }

    public void Start()
    {
        foreach (GameObject gameObject in _gameObjects)
            if (gameObject.Active)
                gameObject.Start();
        
        Active = true;
        SetStarted(true);
    }

    public void SetActive(bool value)
    {
        foreach (GameObject gameObject in _gameObjects)
            gameObject.SetActive(value);

        Active = value;
    }

    public void SetStarted(bool value) =>
        Started = value;

    public GameObject FindGameObjectWithTag(Tags tag) =>
        _gameObjects.FirstOrDefault(x => x.Tag == tag);

    public IReadOnlyList<GameObject> FindGameObjectsWithComponent<T>() =>
        _gameObjects.Where(x => x.ContainsComponent<T>()).ToList();

    public IReadOnlyList<GameObject> FindAllGameObjects<T>() =>
        _gameObjects;

    public void CleanUp()
    {
        _mouseEventSystem.PositionUpdate -= OnPositionUpdate;
        _mouseEventSystem.ClickUpdate -= OnClickUpdate;

        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i]?.Destroy();
    }
}