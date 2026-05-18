using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Camera : Component
{
    public Matrix View { get; private set; }
    public Matrix Projection { get; private set; }
    public GraphicsDevice GraphicsDevice { get; private set; }
    public SpriteBatch SpriteBatch { get; private set; }
    public CameraTypes CameraType { get; private set; }
    public BasicEffect SpriteBatchEffect {get; private set;}
    private readonly GameWindow _window;
    private Vector3 _position;
    private readonly Vector3 _target;
    private Vector3 _up;
    private const int PerspectiveFarPlaneDistance = 1000;
    private const int OrthographicFarDistance = -1;

    public Camera(
            GraphicsDevice graphicsDevice,
            GameWindow window,
            CameraTypes cameraType = CameraTypes.Perspective,
            Vector3 position = new Vector3(),
            Vector3 target = new Vector3(),
            Vector3 up = new Vector3()
            )
    {
        GraphicsDevice = graphicsDevice;
        _window = window;
        CameraType = cameraType;
        _position = position;
        _target = target;
        _up = up;
        SpriteBatch = new SpriteBatch(GraphicsDevice);
        SpriteBatchEffect = new BasicEffect(GraphicsDevice);
    }

    public void Initialize()
    {
        if (_up == Vector3.Zero)
            _up = Vector3.Up;

        if (CameraType == CameraTypes.Orthographic)
            _position = Vector3.Zero;
        
        SpriteBatchEffect.TextureEnabled = true; 

        SetView();
        SetPojectionMatrix();
        SpriteBatchEffect.View = View;
        SpriteBatchEffect.Projection = Projection;
    }

    public void Display(BasicEffect effect)
    {
        effect.View = View;
        effect.Projection = Projection;
    }

    private void SetView()
    {
        View = CameraType == CameraTypes.Perspective
            ? Matrix.CreateLookAt(_position, _target, _up)
            : Matrix.CreateLookAt(_position, _position + Vector3.Forward, _up);
    }

    private void SetPojectionMatrix()
    {
        float screenAspect = _window.ClientBounds.Width / (float)_window.ClientBounds.Height;
        Projection = CameraType == CameraTypes.Perspective
        ? Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, screenAspect, 1, PerspectiveFarPlaneDistance)
        : Matrix.CreateOrthographic(_window.ClientBounds.Width, _window.ClientBounds.Height, 0f, OrthographicFarDistance);
    }
}

public enum CameraTypes
{
    Perspective = 0,
    Orthographic = 1
}