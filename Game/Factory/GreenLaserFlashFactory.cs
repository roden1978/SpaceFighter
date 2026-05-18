using Microsoft.Xna.Framework.Graphics;

public class GreenLaserFlashFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;

    public string Name => "GreenLaserFlash";

    public GreenLaserFlashFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
    }
     public GameObject Create()
    {
        Texture2D texture = _contentProvider.GetTextureByType(TextureTypes.GreenLaserFlash);

        GameObject explosion = new($"GreenLaserFlash");
        
        Sequence sequence = new(new SpriteOrder(texture, CalculateSpritesCount(texture.Width, texture.Height), 1));
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
                                                            75,
                                                            "Flash", false)
                                                    ])
                                                    .SetAnimation("Flash")
                                                );
    }
    private static int CalculateSpritesCount(int width, int height) => width / height;
}
