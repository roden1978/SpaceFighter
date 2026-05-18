using Microsoft.Xna.Framework.Graphics;

public sealed class HealthPillFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;
    private readonly IPositionAdapter _positionAdapter;

    public string Name => "HealthPill";

    public HealthPillFactory(IContentProvider contentProvider, IPositionAdapter positionAdapter)
    {
        _contentProvider = contentProvider;
        _positionAdapter = positionAdapter;
    }
    public GameObject Create()
    {
        GameObject healthPill = new($"HealthPill")
        {
            Layer = LayerMask.GetLayerId(CollisionLayers.Pickeable)
        };
        Texture2D healthPillTexture = _contentProvider.GetTextureByType(TextureTypes.HealthPill); 
        Sprite sprite = new(healthPillTexture);
        VerticalAmplifier verticalAmplifier = new();
        healthPill.AddComponent(new SpriteRenderer(0.5f, sprite))
        .AddComponent(new BoxCollider2D(sprite.Width, sprite.Height, BodyTypes.Static){IsTrigger = true})
        .AddComponent(new Pickeable())
        .AddComponent(new Magnet(_positionAdapter, verticalAmplifier))
        .AddComponent(verticalAmplifier);

        return healthPill;
    }
}
    
