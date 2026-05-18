using Microsoft.Xna.Framework.Graphics;

public sealed class RedLaserFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;

    public string Name => "RedLaser";

    public RedLaserFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
    }
    public GameObject Create()
    {
        GameObject laserGameObject = new($"RedLaser")
        {
            Layer = LayerMask.GetLayerId(CollisionLayers.Laser)
        };
        Texture2D laserTexture = _contentProvider.GetTextureByType(TextureTypes.RedLaser);
        Sprite sprite = new(laserTexture);
        
        ShipLaser laser = new();
        laser.LaserData.LasersType = LasersTypes.RedLaser;
        laser.LaserData.Speed = .8f;
        laser.LaserData.Damage = .2f;
        laserGameObject.AddComponent(new SpriteRenderer(0.2f, sprite))
        .AddComponent(new BoxCollider2D(sprite.Width, sprite.Height, BodyTypes.Dynamic){IsTrigger = true})
        .AddComponent(laser);

        return laserGameObject;
    }
}
    
