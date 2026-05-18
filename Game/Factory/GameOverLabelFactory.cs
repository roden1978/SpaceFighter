using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameOverLabelFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;

    public GameOverLabelFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
    }

    public string Name => GetType().Name;

    public GameObject Create()
    {
        Texture2D gameOverLabel = _contentProvider.GetTextureByType(TextureTypes.GameOverLabel);
        Vector2 position = new(0, 100);
        GameObject gameOverLabelDrawer = new("GameOverLabelDrawer", position, 0, Vector2.One);

        gameOverLabelDrawer
            .AddComponent(new UIImage(new Sprite(gameOverLabel)))
            .AddComponent(new VerticalAmplifier(position));

        gameOverLabelDrawer.SetActive(false);
        return gameOverLabelDrawer;
    }
}
