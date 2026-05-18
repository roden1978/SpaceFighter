
public sealed class BackgroundFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;

    public string Name => "BackgroundFactory";

    public BackgroundFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
    }
    public GameObject Create()
    {
        GameObject background = new($"Background");
        Sprite sprite = _contentProvider.GenerateBackground();
        background.AddComponent(new SpriteRenderer(1f, sprite));

        return background;
    }
}
