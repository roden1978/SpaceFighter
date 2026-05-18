using Microsoft.Xna.Framework.Graphics;

public sealed class GreenLaserFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;

    public string Name => "GreenLaser";

    public GreenLaserFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
    }
    public GameObject Create()
    {
        GameObject laserGameObject = new($"GreenLaser")
        {
            Layer = LayerMask.GetLayerId(CollisionLayers.Laser)
        };
        
        Texture2D laserTexture = _contentProvider.GetTextureByType(TextureTypes.GreenLaser);
        Sprite sprite = new(laserTexture);

        ShipLaser laser = new();
        laser.LaserData.LasersType = LasersTypes.GreenLaser;
        laserGameObject.AddComponent(new SpriteRenderer(0.2f, sprite))
        .AddComponent(new BoxCollider2D(sprite.Width, sprite.Height, BodyTypes.Dynamic){IsTrigger = true})
        .AddComponent(laser);

        return laserGameObject;
    }
}
    
