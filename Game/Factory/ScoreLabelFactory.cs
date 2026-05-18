using Microsoft.Xna.Framework;

public class ScoreLabelFactory : IFactory<GameObject>
{
    public string Name => GetType().Name;
    private readonly IContentProvider _contentProvider;

    public ScoreLabelFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider; 
    }
    public GameObject Create()
    {
        GameObject scoreLabelDrawer = new();

        TextDrawer textDrawer = new (_contentProvider.FontSequence)
        {
            Text = "SCORES",
            TextColor = Color.Blue
        };
        scoreLabelDrawer
            .AddComponent(textDrawer);

        return scoreLabelDrawer;
    }
}
