using Microsoft.Xna.Framework.Graphics;

public class BloodEffectFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;

    public string Name => "Blood";

    public BloodEffectFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
    }
     public GameObject Create()
    {
        Texture2D texture = _contentProvider.GetTextureByType(TextureTypes.BloodEffect);

        GameObject explosion = new($"Blood");
        
        Sequence sequence = new(new SpriteOrder(texture, 7, 1));
        SpriteRenderer spriteRenderer = new(sequence[0]);
                                                            
        explosion.AddComponent(new Effect())
                .AddComponent(spriteRenderer)
                .AddComponent(CreateSplashAnimator(spriteRenderer, sequence));
    
        return explosion;
    }

    private static Animator CreateSplashAnimator(SpriteRenderer spriteRenderer, Sequence sequence)
    {
        return new Animator(spriteRenderer, new AnimationController([
                                                    new Animation(sequence,
                                                            50,
                                                            "Blood", false)
                                                    ])
                                                    .SetAnimation("Blood")
                                                );
    }
}