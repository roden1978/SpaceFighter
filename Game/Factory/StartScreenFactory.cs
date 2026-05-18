using Microsoft.Xna.Framework;

public class StartScreenFactory : IFactory<GameObject>
{
    public string Name => GetType().Name;
    private readonly StartScreenBehaviour _startScreenBehaviour;
    private readonly StartButtonFactory _startButtonFactory;
    private readonly GameOverLabelFactory _gameOverLabelFactory;
    private readonly LeaderboardFactory _leaderboardFactory;
    private readonly TitleFactory _titleFactory;
    private readonly InstructionLabelFactory _instructionLabelFactory;
    private readonly ScoreLabelFactory _scoreLabelFactory;
    private readonly ScoreTextFactory _scoreTextFactory;

    public StartScreenFactory(
        StartScreenBehaviour startScreenBehaviour,
        StartButtonFactory startButtonFactory,
        GameOverLabelFactory gameOverLabelFactory,
        LeaderboardFactory leaderboardFactory,
        TitleFactory titleFactory,
        InstructionLabelFactory instructionLabelFactory,
        ScoreLabelFactory scoreLabelFactory,
        ScoreTextFactory scoreTextFactory
        )
    {
        _startScreenBehaviour = startScreenBehaviour;
        _startButtonFactory = startButtonFactory;
        _gameOverLabelFactory = gameOverLabelFactory;
        _leaderboardFactory = leaderboardFactory;
        _titleFactory = titleFactory;
        _instructionLabelFactory = instructionLabelFactory;
        _scoreLabelFactory = scoreLabelFactory;
        _scoreTextFactory = scoreTextFactory;
    }
    public GameObject Create()
    {
        GameObject startScreen = new($"StartScreen", new(Settings.ScreenWidth / 2, Settings.ScreenHeight / 2), 0, Vector2.One);
        startScreen
        .AddComponent(new CanvasHandler())
        .AddComponent(_startScreenBehaviour);

        Button button = CreateStartButton(startScreen.Transform);
        _startScreenBehaviour.StartButton = button;

        CreateGameOverLabel(startScreen.Transform);
        CreateLeaderboard(startScreen.Transform);
        CreateTitle(startScreen.Transform);
        CreateInstruction(startScreen.Transform);
        CreateScoreLabel(startScreen.Transform);
        CreateScoreText(startScreen.Transform);
        return startScreen;
    }

    private void CreateLeaderboard(Transform2D parent)
    {
        GameObject leaderboard = _leaderboardFactory.Create();
        leaderboard.Transform.Parent = parent;
    }

    private void CreateGameOverLabel(Transform2D parent)
    {
        GameObject gameOverLabel = _gameOverLabelFactory.Create();
        gameOverLabel.Transform.Parent = parent;
    }
    
    private Button CreateStartButton(Transform2D parent)
    {
        GameObject startButton = _startButtonFactory.Create();
        startButton.Transform.Parent = parent;
        return startButton.GetComponent<Button>();
    }

    private void CreateTitle(Transform2D parent)
    {
        GameObject title = _titleFactory.Create();
        title.Transform.Parent = parent;
    }

    private void CreateInstruction(Transform2D parent)
    {
        GameObject instruction = _instructionLabelFactory.Create();
        instruction.Transform.Parent = parent;
    }

    private void CreateScoreText(Transform2D parent)
    {
        GameObject scoreText = _scoreTextFactory.Create();
        TextDrawer textDrawer = scoreText.GetComponent<TextDrawer>();
        textDrawer.TextColor = Color.LightBlue;
        scoreText.Name = "ScoreTextDrawerStartScreen";
        scoreText.Transform.Position = new(120, -250);
        scoreText.Transform.Scale = new(.7f, .7f);
        scoreText.Transform.Parent = parent;
    }

    private void CreateScoreLabel(Transform2D parent)
    {
        GameObject scoreLabel = _scoreLabelFactory.Create();
        TextDrawer textDrawer = scoreLabel.GetComponent<TextDrawer>();
        textDrawer.Text = "RESULT:";
        textDrawer.TextColor = Color.LightBlue;
        scoreLabel.Name = "ScoreLabelDrawerStartScreen";
        scoreLabel.Transform.Position = new(-50, -250);
        scoreLabel.Transform.Scale = new(.7f, .7f);
        scoreLabel.Transform.Parent = parent;
    }
}
