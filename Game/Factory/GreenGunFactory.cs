
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public sealed class GreenGunFactory : IFactory<GameObject>
{
    public string Name => "GreenGunFactory";
    private readonly IContentProvider _contentProvider;
    
    public GreenGunFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
    }
    public GameObject Create()
    {
        GameObject gun = new($"GreenGun");
        
        Texture2D gunTexture = _contentProvider.GetTextureByType(TextureTypes.GunGreen);
        Sprite sprite = new(gunTexture);

        GameObject leftShootPoint = new($"LeftShootPoint", new(-15, 20), 0, Vector2.One, gun.Transform);
        leftShootPoint.AddComponent(new SpriteRenderer(sprite));
        
        GameObject rightShootPoint = new($"RightShootPoint", new(15, 20), 0, Vector2.One, gun.Transform);
        rightShootPoint.AddComponent(new SpriteRenderer(sprite));

        gun.AddComponent(new GunBehaviour(
            new([leftShootPoint.Transform, rightShootPoint.Transform],
            GreenGunStaticData.LaserType,
            GreenGunStaticData.Power,
            GreenGunStaticData.Priority)));
        return gun;
    }
}
