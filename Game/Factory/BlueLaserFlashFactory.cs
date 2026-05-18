using Microsoft.Xna.Framework.Graphics;

public class BlueLaserFlashFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;

    public string Name => "BlueLaserFlash";

    public BlueLaserFlashFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
    }
     public GameObject Create()
    {
        Texture2D texture = _contentProvider.GetTextureByType(TextureTypes.BlueLaserFlash);

        GameObject explosion = new($"BlueLaserFlash");
        
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