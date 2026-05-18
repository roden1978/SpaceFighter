
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public sealed class RedGunFactory : IFactory<GameObject>
{
    public string Name => "RedGunFactory";
    private readonly IContentProvider _contentProvider;
    
    public RedGunFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
    }
    public GameObject Create()
    {
        GameObject gun = new($"RedGun");

        Texture2D gunTexture = _contentProvider.GetTextureByType(TextureTypes.GunRed);
        Sprite sprite = new(gunTexture);

        GameObject leftShootPoint = new($"LeftShootPoint", new(-15, 20), 0, Vector2.One, gun.Transform);
        leftShootPoint.AddComponent(new SpriteRenderer(sprite));

        GameObject rightShootPoint = new($"RightShootPoint", new(15, 20), 0, Vector2.One, gun.Transform);
        rightShootPoint.AddComponent(new SpriteRenderer(sprite));

        GameObject leftShootPoint2 = new($"LeftShootPoint2", new(-30, 10), 0, Vector2.One, gun.Transform);
        leftShootPoint2.AddComponent(new SpriteRenderer(sprite));

        GameObject rightShootPoint2 = new($"RightShootPoint2", new(30, 10), 0, Vector2.One, gun.Transform);
        rightShootPoint2.AddComponent(new SpriteRenderer(sprite));

        gun.AddComponent(new GunBehaviour(new(
            [leftShootPoint.Transform,
            leftShootPoint2.Transform,
            rightShootPoint.Transform,
            rightShootPoint2.Transform], 
            RedGunStaticData.LaserType, 
            RedGunStaticData.Power, 
            RedGunStaticData.Priority)
            ));
        return gun;
    }
}
