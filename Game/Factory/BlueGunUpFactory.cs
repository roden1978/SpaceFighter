using Microsoft.Xna.Framework.Graphics;

public sealed class BlueGunUpFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;
    private readonly IPositionAdapter _positionAdapter;

    public string Name => "BlueGunUp";

    public BlueGunUpFactory(IContentProvider contentProvider, IPositionAdapter positionAdapter)
    {
        _contentProvider = contentProvider;
        _positionAdapter = positionAdapter;
    }
    public GameObject Create()
    {
        GameObject blueGunUp = new($"BlueGunUp")
        {
            Layer = LayerMask.GetLayerId(CollisionLayers.Pickeable)
        };
        Texture2D blueGunUpTexture = _contentProvider.GetTextureByType(TextureTypes.BlueGunUp);
        Sprite sprite = new(blueGunUpTexture);
        VerticalAmplifier verticalAmplifier = new();
        blueGunUp.AddComponent(new SpriteRenderer(0.5f, sprite))
        .AddComponent(new BoxCollider2D(sprite.Width, sprite.Height, BodyTypes.Static){IsTrigger = true})
        .AddComponent(new Pickeable())
        .AddComponent(new Magnet(_positionAdapter, verticalAmplifier))
        .AddComponent(verticalAmplifier);

        return blueGunUp;
    }
}
    
