public sealed class AsteroidsFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;

    public string Name => "Asteroid";

    public AsteroidsFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
    }
    public GameObject Create()
    {
        GameObject asteroid = new($"Asteroid")
        {
            Layer = LayerMask.GetLayerId(CollisionLayers.Asteroid)
        };
        Sprite sprite = _contentProvider.GetRandomMeteorBig();
        asteroid.AddComponent(new SpriteRenderer(0.5f, sprite))
        .AddComponent(new BoxCollider2D(sprite.Width - 20, sprite.Height - 20, BodyTypes.Dynamic) { IsTrigger = true })
        .AddComponent(new Asteroid()
        {
            EnemyData = new()
            {
                Health = AsteroidStaticData.Health,
                Price = AsteroidStaticData.Price,
                Damage = AsteroidStaticData.Damage
            }
        });

        return asteroid;
    }
}

