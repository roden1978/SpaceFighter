using Microsoft.Xna.Framework;

public class ScoreTextFactory : IFactory<GameObject>
{
    public string Name => GetType().Name;
    private readonly IContentProvider _contentProvider;
    private readonly IScoreStringProvider _scoreProvider;

    public ScoreTextFactory(IContentProvider contentProvider, IScoreStringProvider scoreProvider) 
    {
        _contentProvider = contentProvider;
        _scoreProvider = scoreProvider;
    }
    public GameObject Create()
    {
        GameObject scoreTextDrawer = new();
        TextDrawer textDrawer = new (_contentProvider.FontSequence, _scoreProvider.StringSource)
        {
            TextColor = Color.OrangeRed
        };
        scoreTextDrawer
            .AddComponent(textDrawer);

        return scoreTextDrawer;
    }
}
