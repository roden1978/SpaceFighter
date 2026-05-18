using Microsoft.Xna.Framework.Graphics;

public sealed class EnemyLaserFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;

    public string Name => "EnemyLaser";

    public EnemyLaserFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
    }
    public GameObject Create()
    {
        GameObject laserGameObject = new("EnemyLaser")
        {
            Layer = LayerMask.GetLayerId(CollisionLayers.EnemyLaser)
        };
        Texture2D laserTexture = _contentProvider.GetTextureByType(TextureTypes.RedLaser);
        Sprite sprite = new(laserTexture);
        
        EnemyLaser laser = new();
        laser.LaserData.LasersType = LasersTypes.EnemyLaser;
        laser.LaserData.Speed = 1f;
        laser.LaserData.Damage = .2f;
        laserGameObject.AddComponent(new SpriteRenderer(0.55f, sprite))
        .AddComponent(new BoxCollider2D(sprite.Width, sprite.Height, BodyTypes.Dynamic){IsTrigger = true})
        .AddComponent(laser);

        return laserGameObject;
    }
}
    
