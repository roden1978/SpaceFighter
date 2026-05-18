using Microsoft.Xna.Framework.Graphics;

public sealed class EnergyPillFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;
    private readonly IPositionAdapter _positionAdapter;

    public string Name => "EnergyPill";

    public EnergyPillFactory(IContentProvider contentProvider, IPositionAdapter positionAdapter)
    {
        _contentProvider = contentProvider;
        _positionAdapter = positionAdapter;
    }
    public GameObject Create()
    {
        GameObject energyPill = new($"EnergyPill")
        {
            Layer = LayerMask.GetLayerId(CollisionLayers.Pickeable)
        };
        Texture2D energyPillTexture = _contentProvider.GetTextureByType(TextureTypes.EnergyPill);
        Sprite sprite = new(energyPillTexture);
        VerticalAmplifier verticalAmplifier = new();
        energyPill.AddComponent(new SpriteRenderer(0.5f, sprite))
        .AddComponent(new BoxCollider2D(sprite.Width, sprite.Height, BodyTypes.Static){IsTrigger = true})
        .AddComponent(new Pickeable())
        .AddComponent(new Magnet(_positionAdapter, verticalAmplifier))
        .AddComponent(verticalAmplifier);

        return energyPill;
    }
}
    
