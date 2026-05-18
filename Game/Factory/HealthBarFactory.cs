using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HealthBarFactory : IFactory<GameObject>
{
    public string Name => GetType().Name;
    private readonly IContentProvider _contentProvider;
    private readonly IShipHealthProvider _healthProvider;

    public HealthBarFactory(IContentProvider contentProvider,  IShipHealthProvider healthProvider)
    {
        _contentProvider = contentProvider;
        _healthProvider = healthProvider; 
    }
    public GameObject Create()
    {
        GameObject healthBar = new("HealthBar", new(130, 10), 0, Vector2.One);
        Sprite backgroundSprite = _contentProvider.CreateDefaultSprite(204, 18, Color.Gray);
        Sprite sliderSprite = _contentProvider.CreateDefaultSprite(200, 14);
        healthBar.AddComponent(new Slider(backgroundSprite, sliderSprite, Color.Green, Color.Red, _healthProvider.Health));

        CreateHealthBarIcon(healthBar.Transform);
        return healthBar;
    }

    private void CreateHealthBarIcon(Transform2D parent)
    {
        Texture2D iconTexture = _contentProvider.GetTextureByType(TextureTypes.LifeIcon);
        GameObject icon = new("HealthBarIcon", new(-100, 0), 0, Vector2.One);
        icon.Transform.Parent = parent;
        icon.AddComponent(new UIImage(new(iconTexture)));
    }
}
