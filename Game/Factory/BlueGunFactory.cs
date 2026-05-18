
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public sealed class BlueGunFactory : IFactory<GameObject>
{
    public string Name => "BlueGunFactory";
    private readonly IContentProvider _contentProvider;
    

    public BlueGunFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
    }

    public GameObject Create()
    {
        GameObject gun = new($"BlueGun");


        Texture2D gunBlueTexture = _contentProvider.GetTextureByType(TextureTypes.GunBlue);
        Sprite sprite = new(gunBlueTexture);

        Texture2D gunTexture = _contentProvider.GetTextureByType(TextureTypes.GunRed);
        Sprite redSprite = new(gunTexture);

        GameObject centerPoint = new($"CenterShootPoint", new(0, 40), 0, Vector2.One, gun.Transform);
        centerPoint.AddComponent(new SpriteRenderer(redSprite));

        GameObject leftShootPoint = new($"LeftShootPoint", new(-20, 20), 0, Vector2.One, gun.Transform);
        leftShootPoint.AddComponent(new SpriteRenderer(sprite));

        GameObject rightShootPoint = new($"RightShootPoint", new(20, 20), 0, Vector2.One, gun.Transform);
        rightShootPoint.AddComponent(new SpriteRenderer(sprite){Flip = true});

        GameObject leftShootPoint2 = new($"LeftShootPoint2", new(-35, 15), 0, Vector2.One, gun.Transform);
        leftShootPoint2.AddComponent(new SpriteRenderer(sprite));

        GameObject rightShootPoint2 = new($"RightShootPoint2", new(35, 15), 0, Vector2.One, gun.Transform);
        rightShootPoint2.AddComponent(new SpriteRenderer(sprite){Flip = true});

        gun.AddComponent(new GunBehaviour(new(
            [leftShootPoint.Transform,
            leftShootPoint2.Transform,
            rightShootPoint.Transform,
            rightShootPoint2.Transform,
            centerPoint.Transform],
            BlueGunStaticData.LaserType,
            BlueGunStaticData.Power,
            BlueGunStaticData.Priority
            )));
        return gun;
    }
}
