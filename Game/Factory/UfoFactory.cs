using Microsoft.Xna.Framework.Graphics;

public sealed class UfoFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;
    private readonly ILaserDispatcher _laserDispatcher;
    private readonly IPositionAdapter _positionAdapter;

    public string Name => "Ufo";

    public UfoFactory(IContentProvider contentProvider, ILaserDispatcher laserDispatcher, IPositionAdapter positionAdapter)
    {
        _contentProvider = contentProvider;
        _laserDispatcher = laserDispatcher;
        _positionAdapter = positionAdapter;
    }
    public GameObject Create()
    {
        GameObject ufo = new($"Ufo")
        {
            Layer = LayerMask.GetLayerId(CollisionLayers.Ufo)
        };
        Texture2D ufoTexture = _contentProvider.GetTextureByType(TextureTypes.Ufo); 
        Sequence sequence = new(new SpriteOrder(ufoTexture, 4,1));
        SpriteRenderer spriteRenderer = new (0.5f, sequence[0]); 
        ufo.AddComponent(spriteRenderer)
        .AddComponent(new BoxCollider2D(sequence[0].Width - 5, sequence[0].Height - 5, BodyTypes.Dynamic){IsTrigger = true})
        .AddComponent(CreateUfoAnimation(spriteRenderer, sequence))
        .AddComponent(new Ufo(_laserDispatcher, _positionAdapter) {
            EnemyData = new()
            {
                Health = UfoStaticData.Health,
                Price = UfoStaticData.Price,
                Damage = UfoStaticData.Damage,
                ShootDelay = UfoStaticData.ShootDelay
            }
        });

        return ufo;
    }

    private Animator CreateUfoAnimation(SpriteRenderer spriteRenderer, Sequence sequence)
    {
        return new Animator(spriteRenderer, 
        new AnimationController(
            [
                new Animation(sequence, 500, "UfoMove")
            ]
        )
        .SetAnimation("UfoMove"));
    }
}
    
