using System;

public sealed class GameController : Component
{
    private readonly IPersistentProgressService _persistentProgressService;
    private readonly IResultProvider _resultProvider;
    private readonly IEnemyDispatcher _enemyDispatcher;
    private readonly ILaserDispatcher _laserDispatcher;
    private readonly ShipModel _shipModel;
    private readonly IScoreStringProvider _scoreStringProvider;
    private readonly IBonusesDispatcher _bonusesDispatcher;
    private readonly IEffectDispatcher _effectDispatcher;
    private readonly IShipActivator _shipActivator;
    private readonly HUD _hud;
    private readonly StartScreenBehaviour _startScreenBehaviour;

    public GameController(
        IPersistentProgressService persistentProgressService,
        IResultProvider resultProvider,
        IEnemyDispatcher enemyDispatcher,
        ILaserDispatcher laserDispatcher,
        ShipModel shipModel,
        IScoreStringProvider scoreStringProvider,
        IBonusesDispatcher bonusesDispatcher,
        IEffectDispatcher effectDispatcher,
        IShipActivator shipActivator,
        HUD hud,
        StartScreenBehaviour startScreenBehaviour)
    {
        _persistentProgressService = persistentProgressService;
        _resultProvider = resultProvider;
        _enemyDispatcher = enemyDispatcher;
        _laserDispatcher = laserDispatcher;
        _shipModel = shipModel;
        _shipModel.ShipDeath += OnShipDeath;
        _scoreStringProvider = scoreStringProvider;
        _bonusesDispatcher = bonusesDispatcher;
        _effectDispatcher = effectDispatcher;
        _effectDispatcher.GameOver += OnGameOver;
        _shipActivator = shipActivator;
        _hud = hud;
        _startScreenBehaviour = startScreenBehaviour;
    }

    public override void Start()
    {
        _startScreenBehaviour.StartButton.AddListener(OnStartButtonClick);
    }

    private void SetActiveStartScreen(bool value)
    {
        _startScreenBehaviour.gameObject.SetActive(value);
    }

    private void SetActiveHUD(bool value)
    {
        _hud.gameObject.SetActive(value);
    }
    private void OnShipDeath()
    {
        _shipActivator.SetActive(false);
        _shipModel.ActivateShip(false);
        SetActiveHUD(false);
    }

    private void OnGameOver()
    {
        DisabledDispatchers();
        SaveResult();
        SetActiveStartScreen(true);
    }

    public void StartGame()
    {
        SetActiveHUD(true);
        SetActiveStartScreen(false);
        _scoreStringProvider.StringSource.Value = (_resultProvider.Result.Value = 0).ToString();
        EnabledDispatchers();
        _shipModel.ActivateShip(true);

        if (false == _shipActivator.Active)
            _shipActivator.SetActive(true);
    }

    private void EnabledDispatchers()
    {
        _enemyDispatcher.ActivateDispatcher(true);
        _laserDispatcher.ActivateDispatcher(true);
        _bonusesDispatcher.ActivateDispatcher(true);
    }

    private void DisabledDispatchers()
    {
        _bonusesDispatcher.ActivateDispatcher(false);
        _enemyDispatcher.ActivateDispatcher(false);
        _laserDispatcher.ActivateDispatcher(false);
    }

    private void SaveResult()
    {
        _persistentProgressService.Save(_resultProvider.Result);
    }

    private void OnStartButtonClick(UIEvent uIEvent)
    {
        StartGame();
    }

    public override void Destroy()
    {
        _shipModel.ShipDeath -= OnShipDeath;
        _effectDispatcher.GameOver -= OnGameOver;
        _startScreenBehaviour.StartButton.RemoveListener(OnStartButtonClick);
    }
}