using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class StartButtonFactory : IFactory<GameObject>
{
    public string Name => GetType().Name;
    private readonly IContentProvider _contentProvider;

    public StartButtonFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
    }
    public GameObject Create()
    {
        GameObject startButton = new("StartButton", new(0, 200), 0, new(.8f, .8f));

        Texture2D startButtonTexture = _contentProvider.GetTextureByType(TextureTypes.PlayButton);
        startButton
        .AddComponent(new Button(new Sprite(startButtonTexture), Color.White, Color.Green, Color.White))
        .AddComponent(new StartButtonBehaviour());
        return startButton;
    }
}
