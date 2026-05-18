using Microsoft.Xna.Framework.Graphics;

public class HealthBoosterEffectFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;

    public string Name => "HealthBooster";

    public HealthBoosterEffectFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
    }
     public GameObject Create()
    {
        Texture2D texture = _contentProvider.GetTextureByType(TextureTypes.HealthBooster);

        GameObject healthBooster = new($"HealthBooster");
        
        Sequence sequence = new(new SpriteOrder(texture, CalculateSpritesCount(texture.Width, texture.Height), 1));
        SpriteRenderer spriteRenderer = new(sequence[0]);
                                                            
        healthBooster.AddComponent(new Effect())
                .AddComponent(spriteRenderer)
                .AddComponent(CreateSplashAnimator(spriteRenderer, sequence));
    
        return healthBooster;
    }

    private static Animator CreateSplashAnimator(SpriteRenderer spriteRenderer, Sequence sequence)
    {
        return new Animator(spriteRenderer, new AnimationController([
                                                    new Animation(sequence,
                                                            75,
                                                            "Booster", false)
                                                    ])
                                                    .SetAnimation("Booster")
                                                );
    }
    private static int CalculateSpritesCount(int width, int height) => width / height;
}
