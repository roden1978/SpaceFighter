using Microsoft.Xna.Framework.Graphics;

public sealed class ShieldFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;

    public string Name => "Shield";

    public ShieldFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
    }
    public GameObject Create()
    {
        GameObject shield = new($"Shield")
        {
            Layer = LayerMask.GetLayerId(CollisionLayers.Shield)
        };
        Texture2D shieldTexture = _contentProvider.GetTextureByType(TextureTypes.Shield);
        Sequence sequence = new(new SpriteOrder(shieldTexture, 4, 1)); 
        SpriteRenderer spriteRenderer = new(0.05f, sequence[0]);
        shield.AddComponent(spriteRenderer)
        .AddComponent(new BoxCollider2D(sequence[0].Width, sequence[0].Height, BodyTypes.Static){IsTrigger = true})
        .AddComponent(CreateShieldAnimator(spriteRenderer, sequence))
        .AddComponent(new Shield());

        return shield;
    }

    private static Animator CreateShieldAnimator(SpriteRenderer spriteRenderer, Sequence sequence)
    {
        return new Animator(spriteRenderer, new AnimationController([
                                                    new Animation(sequence,
                                                            100,
                                                            "ShieldShine")
                                                    ])
                                                    .SetAnimation("ShieldShine")
                                                );
    }
}
    
