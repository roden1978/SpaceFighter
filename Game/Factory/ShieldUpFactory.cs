using Microsoft.Xna.Framework.Graphics;

public sealed class ShieldUpFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;
    private readonly IPositionAdapter _positionAdapter;

    public string Name => "ShieldUp";

    public ShieldUpFactory(IContentProvider contentProvider, IPositionAdapter positionAdapter)
    {
        _contentProvider = contentProvider;
        _positionAdapter = positionAdapter;
    }
    public GameObject Create()
    {
        GameObject shieldUp = new($"ShieldUp")
        {
            Layer = LayerMask.GetLayerId(CollisionLayers.Pickeable)
        };
        Texture2D shieldUpTexture = _contentProvider.GetTextureByType(TextureTypes.ShieldUp); 
        Sprite sprite = new(shieldUpTexture);
        VerticalAmplifier verticalAmplifier = new();
        shieldUp.AddComponent(new SpriteRenderer(0.5f, sprite))
        .AddComponent(new BoxCollider2D(sprite.Width, sprite.Height, BodyTypes.Static){IsTrigger = true})
        .AddComponent(new Pickeable())
        .AddComponent(new Magnet(_positionAdapter, verticalAmplifier))
        .AddComponent(verticalAmplifier);

        return shieldUp;
    }
}
    
