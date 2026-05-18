using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public sealed class ShipFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;
    private readonly Ship _ship;
    private readonly GreenGunFactory _greenGunFactory;
    private readonly RedGunFactory _redGunFactory;
    private readonly BlueGunFactory _blueGunFactory;
    private readonly ILaserDispatcher _laserDispatcher;
    private readonly MouseEventSystem _mouseEventSystem;
    private readonly IShooteable _shooteable;
    private Texture2D _shipAnimation;

    public string Name => GetType().Name;

    public ShipFactory(IContentProvider contentProvider,
            Ship ship,
            GreenGunFactory greenGunFactory,
            RedGunFactory redGunFactory,
            BlueGunFactory blueGunFactory,
            ILaserDispatcher laserDispatcher,
            MouseEventSystem mouseEventSystem,
            IShooteable shooteable)
    {
        _contentProvider = contentProvider;
        _ship = ship;
        _greenGunFactory = greenGunFactory;
        _redGunFactory = redGunFactory;
        _blueGunFactory = blueGunFactory;
        _laserDispatcher = laserDispatcher;
        _mouseEventSystem = mouseEventSystem;
        _shooteable = shooteable;
    }
    public GameObject Create()
    {
        _shipAnimation = _contentProvider.GetTextureByType(TextureTypes.Ship);
        Sequence sequence = new(new SpriteOrder(_shipAnimation, 5, 1));
        //Vector2 position = new(0, -Settings.ScreenHeight / 10); //  + sequence[0].Height * 2
        GameObject ship = new("Ship");//, position, 0, Vector2.One);
        SpriteRenderer spriteRenderer = new(.1f, sequence[0]);
        GunsController gunsController = new(_ship, _laserDispatcher, _mouseEventSystem, _shooteable);
        
        (LasersTypes type, GunBehaviour gunBehaviour) greenGun = CreateGreenGun(ship.Transform);
                
        gunsController.AddWeapon(greenGun)
            .AddWeapon(CreateRedGun(ship.Transform))
            .AddWeapon(CreateBlueGun(ship.Transform));
        gunsController.InitialaizeCurrentGun(greenGun.gunBehaviour);
        gunsController.SetDefaultGun(greenGun.gunBehaviour);

        ship.AddComponent(spriteRenderer)
            .AddComponent(new BoxCollider2D(sequence[0].Width, sequence[0].Height / 2, BodyTypes.Dynamic) { IsTrigger = true, Offset = new(0, 10) })
            .AddComponent(CreatePlayerAnimator(spriteRenderer, sequence))
            .AddComponent(_ship)
            .AddComponent(gunsController);

        return ship;
    }

    private Animator CreatePlayerAnimator(SpriteRenderer spriteRenderer, Sequence sequence)
    {
        return new Animator(spriteRenderer, new AnimationController([
                                                            new Animation(sequence,
                                                            50,
                                                            "Move")
                                                    ])
                                                    .SetAnimation("Move")
                                                );
    }

    private (LasersTypes type, GunBehaviour gunBehaviour) CreateGreenGun(Transform2D parent)
    {
        GameObject gun = _greenGunFactory.Create();
        gun.Transform.Parent = parent;
        GunBehaviour gunBehaviour = gun.GetComponent<GunBehaviour>();
        LasersTypes type = gunBehaviour.GunData.LaserType;
        return (type, gunBehaviour);
    }
    private (LasersTypes type, GunBehaviour gunBehaviour) CreateRedGun(Transform2D parent)
    {
        GameObject gun = _redGunFactory.Create();
        gun.SetActive(false);
        gun.Transform.Parent = parent;
        GunBehaviour gunBehaviour = gun.GetComponent<GunBehaviour>();
        LasersTypes type = gunBehaviour.GunData.LaserType;
        return (type, gunBehaviour);
    }
    private (LasersTypes type, GunBehaviour gunBehaviour) CreateBlueGun(Transform2D parent)
    {
        GameObject gun = _blueGunFactory.Create();
        gun.SetActive(false);
        gun.Transform.Parent = parent;
        GunBehaviour gunBehaviour = gun.GetComponent<GunBehaviour>();
        LasersTypes type = gunBehaviour.GunData.LaserType;
        return (type, gunBehaviour);
    }
}
