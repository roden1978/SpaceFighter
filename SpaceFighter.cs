using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class SpaceFighter : Game, IGraphicsDeviceProvider
{
    private readonly GraphicsDeviceManager _graphics;
    private Bootstraper _bootstraper;
    public SpaceFighter()
    {
        _graphics = new GraphicsDeviceManager(this);
        
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        InitializeGraphicsDevice();

        Window.Title = "Space Fighter";

        _bootstraper = new(this);
        Components.Add(_bootstraper);

        base.Initialize();
    }

    private void InitializeGraphicsDevice()
    {
        // TODO: Add your initialization logic here

        _graphics.GraphicsProfile = GraphicsProfile.Reach;
        _graphics.PreferredBackBufferWidth = Settings.ScreenWidth;
        _graphics.PreferredBackBufferHeight = Settings.ScreenHeight;
        _graphics.ApplyChanges();
    }

    protected override void LoadContent()
    {
        // Content loading from Content Load Service
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        base.Draw(gameTime);
    }

    protected override void Dispose(bool disposing)
    {
        _bootstraper.CleanUp();
        base.Dispose(disposing);
    }
}
