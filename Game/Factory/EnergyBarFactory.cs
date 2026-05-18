using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class EnergyBarFactory : IFactory<GameObject>
{
    public string Name => GetType().Name;
    private readonly IContentProvider _contentProvider;
    private readonly IShipEnergyProvider _shipEnergyProvider;

    public EnergyBarFactory(IContentProvider contentProvider, IShipEnergyProvider shipEnergyProvider)
    {
        _contentProvider = contentProvider;
        _shipEnergyProvider = shipEnergyProvider;
    }
    public GameObject Create()
    {
        GameObject energyBar = new("EnergyBar", new(150, 50), 0, Vector2.One);
        Sprite backgroundSprite = _contentProvider.CreateDefaultSprite(204, 18, Color.White);
        Sprite sliderSprite = _contentProvider.CreateDefaultSprite(200, 14);
        energyBar.AddComponent(new Slider(backgroundSprite, sliderSprite, Color.Yellow, Color.White, _shipEnergyProvider.Energy));

        CreateEnergyBarIcon(energyBar.Transform);
        return energyBar;
    }

    private void CreateEnergyBarIcon(Transform2D parent)
    {
        Texture2D iconTexture = _contentProvider.GetTextureByType(TextureTypes.EnergyIcon);
        GameObject icon = new("EnergyBarIcon", new(-100, 0), 0, Vector2.One);
        icon.Transform.Parent = parent;
        icon.AddComponent(new UIImage(new(iconTexture)));
    }
}
