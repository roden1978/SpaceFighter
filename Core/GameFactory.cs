using System.Collections.Generic;
using Autofac;

public class GameFactory : IFactory<IReadOnlyList<GameObject>>, IStartable
{
    private readonly PoolService _poolService;
    private readonly ShipFactory _shipFactory;
    private readonly AsteroidsFactory _asteroidsFactory;
    private readonly BackgroundFactory _backgroundFactory;
    private readonly GreenLaserFactory _greenLaserFactory;
    private readonly RedLaserFactory _redLaserFactory;
    private readonly BlueLaserFactory _blueLaserFactory;
    private readonly HUDFactory _hudFactory;
    private readonly BackgroundStarsFactory _backgroundStarsFactory;
    private readonly UfoFactory _ufoFactory;
    private readonly EnemyLaserFactory _enemyLaserFactory;
    private readonly ExplosionFactory _explosionFactory;
    private readonly HealthPillFactory _healthPillFactory;
    private readonly EnergyPillFactory _energyPillFactory;
    private readonly ShieldFactory _shieldFactory;
    private readonly RedGunUpFactory _redGunUpFactory;
    private readonly BlueGunUpFactory _blueGunUpFactory;

    private readonly AlienFactory _alienFactory;
    private readonly BloodEffectFactory _bloodEffectFactory;
    private readonly DispatchersObserverFactory _dispatcherObserverFactory;
    private readonly ShieldUpFactory _shieldUpFactory;
    private readonly GreenLaserFlashFactory _greenLaserFlashFactory;
    private readonly RedLaserFlashFactory _redLaserFlashFactory;
    private readonly BlueLaserFlashFactory _blueLaserFlashFactory;
    private readonly HealthBoosterEffectFactory _healthBoosterEffectFactory;
    private readonly EnergyBoosterEffectFactory _energyBoosterEffectFactory;
    private readonly StartScreenFactory _startScreenFactory;
    private readonly ShipExplosionFactory _shipExplosionFactory;
    private readonly GameControllerFactory _gameControllerFactory;
    private readonly BurstFactory _burstFactory;

    public string Name => GetType().Name;

    public GameFactory(PoolService poolService,
        ShipFactory shipFactory,
        AsteroidsFactory asteroidsFactory,
        BackgroundFactory backgroundFactory,
        GreenLaserFactory greenLaserFactory,
        RedLaserFactory redLaserFactory,
        BlueLaserFactory blueLaserFactory,
        HUDFactory hudFactory,
        BackgroundStarsFactory backgroundStarsFactory,
        UfoFactory ufoFactory,
        EnemyLaserFactory enemyLaserFactory,
        ExplosionFactory explosionFactory,
        HealthPillFactory healthPillFactory,
        EnergyPillFactory energyPillFactory,
        ShieldFactory shieldFactory,
        RedGunUpFactory redGunUpFactory,
        BlueGunUpFactory blueGunUpFactory,
        AlienFactory alienFactory,
        BloodEffectFactory bloodEffectFactory,
        DispatchersObserverFactory dispatchersObserverFactory,
        ShieldUpFactory shieldUpFactory,
        GreenLaserFlashFactory greenLaserFlashFactory,
        RedLaserFlashFactory redLaserFlashFactory,
        BlueLaserFlashFactory blueLaserFlashFactory,
        HealthBoosterEffectFactory healthBoosterEffectFactory,
        EnergyBoosterEffectFactory energyBoosterEffectFactory,
        StartScreenFactory startScreenFactory,
        ShipExplosionFactory shipExplosionFactory,
        GameControllerFactory gameControllerFactory,
        BurstFactory burstFactory)
    {
        _poolService = poolService;
        _shipFactory = shipFactory;
        _asteroidsFactory = asteroidsFactory;
        _backgroundFactory = backgroundFactory;
        _greenLaserFactory = greenLaserFactory;
        _redLaserFactory = redLaserFactory;
        _blueLaserFactory = blueLaserFactory;
        _hudFactory = hudFactory;
        _backgroundStarsFactory = backgroundStarsFactory;
        _ufoFactory = ufoFactory;
        _enemyLaserFactory = enemyLaserFactory;
        _explosionFactory = explosionFactory;
        _healthPillFactory = healthPillFactory;
        _energyPillFactory = energyPillFactory;
        _shieldFactory = shieldFactory;
        _redGunUpFactory = redGunUpFactory;
        _blueGunUpFactory = blueGunUpFactory;
        _alienFactory = alienFactory;
        _bloodEffectFactory = bloodEffectFactory;
        _dispatcherObserverFactory = dispatchersObserverFactory;
        _shieldUpFactory = shieldUpFactory;
        _greenLaserFlashFactory = greenLaserFlashFactory;
        _redLaserFlashFactory = redLaserFlashFactory;
        _blueLaserFlashFactory = blueLaserFlashFactory;
        _healthBoosterEffectFactory = healthBoosterEffectFactory;
        _energyBoosterEffectFactory = energyBoosterEffectFactory;
        _startScreenFactory = startScreenFactory;
        _shipExplosionFactory = shipExplosionFactory;
        _gameControllerFactory = gameControllerFactory;
        _burstFactory = burstFactory;
    }
    public IReadOnlyList<GameObject> Create()
    {
        GameObject background = _backgroundFactory.Create();
        GameObject ship = _shipFactory.Create();
        GameObject hud = _hudFactory.Create();
        GameObject dispatherObserver = _dispatcherObserverFactory.Create();
        GameObject startScreen = _startScreenFactory.Create();
        GameObject gameController = _gameControllerFactory.Create();
        
        return [ship, background, hud, dispatherObserver, startScreen, gameController];
    }

    public void Start() => 
        Initialize();
    private void Initialize()
    {
        _poolService.Add(new(_asteroidsFactory, 3))
                    .Add(new(_ufoFactory, 3))
                    .Add(new(_greenLaserFactory, 12))
                    .Add(new(_redLaserFactory, 20))
                    .Add(new(_blueLaserFactory, 30))
                    .Add(new(_enemyLaserFactory, 3))
                    .Add(new(_backgroundStarsFactory, 25, false))
                    .Add(new(_explosionFactory, 3))
                    .Add(new(_healthPillFactory, 3))
                    .Add(new(_energyPillFactory, 3))
                    .Add(new(_shieldFactory, 2))
                    .Add(new(_redGunUpFactory, 3))
                    .Add(new(_blueGunUpFactory, 3))
                    .Add(new(_alienFactory, 3))
                    .Add(new(_bloodEffectFactory, 2))
                    .Add(new(_shieldUpFactory, 3))
                    .Add(new(_greenLaserFlashFactory, 12))
                    .Add(new(_redLaserFlashFactory, 16))
                    .Add(new(_blueLaserFlashFactory, 20))
                    .Add(new(_energyBoosterEffectFactory, 2))
                    .Add(new(_healthBoosterEffectFactory, 2))
                    .Add(new(_shipExplosionFactory, 1))
                    .Add(new(_burstFactory, 3));

        _poolService.Initialize();
    }

}
