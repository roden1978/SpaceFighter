using Microsoft.Xna.Framework.Graphics;
public sealed class AlienFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;

    public string Name => "Alien";

    public AlienFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
    }
    public GameObject Create()
    {
        GameObject alien = new($"Alien")
        {
            Layer = LayerMask.GetLayerId(CollisionLayers.Alien)
        };
        Texture2D alienTexture = _contentProvider.GetTextureByType(TextureTypes.Alien);
        Sequence sequence = new(new SpriteOrder(alienTexture, 2, 1));
        SpriteRenderer spriteRenderer = new(0.5f, sequence[0]);
        alien.AddComponent(spriteRenderer)
        .AddComponent(new BoxCollider2D(sequence[0].Width - 7, sequence[0].Height, BodyTypes.Static) { IsTrigger = true })
        .AddComponent(CreateAlienAnimation(spriteRenderer, sequence))
        .AddComponent(new Pickeable())
        .AddComponent(new VerticalAmplifier());

        return alien;
    }

    private Animator CreateAlienAnimation(SpriteRenderer spriteRenderer, Sequence sequence)
    {
        return new Animator(spriteRenderer,
        new AnimationController(
            [
                new Animation(sequence, 500, "Alien")
            ]
        )
        .SetAnimation("Alien"));
    }
}

