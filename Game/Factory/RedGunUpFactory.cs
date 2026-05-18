using Microsoft.Xna.Framework.Graphics;

public sealed class RedGunUpFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;
    private readonly IPositionAdapter _positionAdapter;

    public string Name => "RedGunUp";

    public RedGunUpFactory(IContentProvider contentProvider, IPositionAdapter positionAdapter)
    {
        _contentProvider = contentProvider;
        _positionAdapter = positionAdapter;
    }
    public GameObject Create()
    {
        GameObject redGunUp = new($"RedGunUp")
        {
            Layer = LayerMask.GetLayerId(CollisionLayers.Pickeable)
        };
        Texture2D redGunUpTexture = _contentProvider.GetTextureByType(TextureTypes.RedGunUp);
        Sprite sprite = new(redGunUpTexture);
        VerticalAmplifier verticalAmplifier = new();
        redGunUp.AddComponent(new SpriteRenderer(0.5f, sprite))
        .AddComponent(new BoxCollider2D(sprite.Width, sprite.Height, BodyTypes.Static){IsTrigger = true})
        .AddComponent(new Pickeable())
        .AddComponent(new Magnet(_positionAdapter, verticalAmplifier))
        .AddComponent(verticalAmplifier);

        return redGunUp;
    }
}
    
