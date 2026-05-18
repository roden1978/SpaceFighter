using Autofac;
using Microsoft.Xna.Framework;

public class HUDFactory : IFactory<GameObject>
{
    public string Name => GetType().Name;
    private readonly ScoreLabelFactory _scoreLabelFactory;
    private readonly ScoreTextFactory _scoreTextFactory;
    private readonly HealthBarFactory _healthBarFactory;
    private readonly EnergyBarFactory _energyBarFactory;
    private readonly HUD _hud;

    public HUDFactory(ScoreLabelFactory scoreLabelFactory,
        ScoreTextFactory scoreTextFactory,
        HealthBarFactory healthBarFactory,
        EnergyBarFactory energyBarFactory,
        HUD hud)
    {
        _scoreLabelFactory = scoreLabelFactory;
        _scoreTextFactory = scoreTextFactory;
        _healthBarFactory = healthBarFactory;
        _energyBarFactory = energyBarFactory;
        _hud = hud;
    }
    public GameObject Create()
    {
        GameObject hud = new($"HUD", new Vector2(0, 20), 0, Vector2.One);
        hud.AddComponent(_hud);

        CreateScoreLabel(hud.Transform);
        CreateScoreText(hud.Transform);
        CreateHealthBar(hud.Transform);
        CreateEnergyBar(hud.Transform);
        hud.SetActive(false);
        return hud;
    }

    private void CreateScoreText(Transform2D parent)
    {
        GameObject scoreText = _scoreTextFactory.Create();
        scoreText.Name = "ScoreTextDrawer";
        scoreText.Transform.Position = new(Settings.ScreenWidth / 2 + 180, 30);
        scoreText.Transform.Parent = parent;
    }

    private void CreateScoreLabel(Transform2D parent)
    {
        GameObject scoreLabel = _scoreLabelFactory.Create();
        scoreLabel.Name = "ScoreLabelDrawer";
        scoreLabel.Transform.Position = new(Settings.ScreenWidth / 2 + 100, 0);
        scoreLabel.Transform.Scale = new(.5f, .5f);
        scoreLabel.Transform.Parent = parent;
    }

    private void CreateHealthBar(Transform2D parent)
    {
        GameObject healthBar = _healthBarFactory.Create();
        healthBar.Transform.Parent = parent;
    }
    private void CreateEnergyBar(Transform2D parent)
    {
        GameObject energyBar = _energyBarFactory.Create();
        energyBar.Transform.Parent = parent;
    }
}
