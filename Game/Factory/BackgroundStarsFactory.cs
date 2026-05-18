using System;

public class BackgroundStarsFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;
    private readonly Random _random;

    public string Name => "BackgroundStar";

    public BackgroundStarsFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
        _random = new();
    }
    public GameObject Create()
    {
        GameObject backgroundStar = new("BackgroundStar");
        Sprite sprite = _contentProvider.CreateDefaultSprite(_random.Next(1, 3), _random.Next(1, 6));
        backgroundStar.AddComponent(new SpriteRenderer(.99f, sprite, .9f))
        .AddComponent(new BackgroundStar());
        return backgroundStar; 
    }
}
