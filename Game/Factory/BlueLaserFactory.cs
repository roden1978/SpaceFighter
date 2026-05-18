using Microsoft.Xna.Framework.Graphics;

public sealed class BlueLaserFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;

    public string Name => "BlueLaser";

    public BlueLaserFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
    }
    public GameObject Create()
    {
        GameObject laserGameObject = new($"BlueLaser")
        {
            Layer = LayerMask.GetLayerId(CollisionLayers.Laser)
        };
        
        Texture2D laserTexture = _contentProvider.GetTextureByType(TextureTypes.BlueLaser);
        Sprite sprite = new(laserTexture);
        
        ShipLaser laser = new();
        laser.LaserData.LasersType = LasersTypes.BlueLaser;
        laser.LaserData.Speed = 1f;
        laser.LaserData.Damage = .3f;
        laserGameObject.AddComponent(new SpriteRenderer(0.2f, sprite))
        .AddComponent(new BoxCollider2D(sprite.Width, sprite.Height, BodyTypes.Dynamic){IsTrigger = true})
        .AddComponent(laser);

        return laserGameObject;
    }
}
    
