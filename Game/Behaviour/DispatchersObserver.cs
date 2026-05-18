using Microsoft.Xna.Framework;

public sealed class DispatchersObserver : Component
{
    private readonly ILaserDispatcher _laserDispatcher;
    private readonly IBonusesDispatcher _bonusesDispatcher;
    private readonly IBackgroundStarsDispatcher _backgroundStarsDispatcher;
    private readonly IEffectDispatcher _effectDispatcher;
    private readonly IEnemyDispatcher _enemyDispatcher;
    private readonly IShieldDispatcher _shieldDispatcher;

    public DispatchersObserver(ILaserDispatcher laserDispatcher, 
        IBonusesDispatcher bonusesDispatcher, 
        IBackgroundStarsDispatcher backgroundStarsDispatcher,
        IEffectDispatcher effectDispatcher, 
        IEnemyDispatcher enemyDispatcher,
        IShieldDispatcher shieldDispatcher)
    {
        _laserDispatcher = laserDispatcher;
        _bonusesDispatcher = bonusesDispatcher;
        _backgroundStarsDispatcher = backgroundStarsDispatcher;
        _effectDispatcher = effectDispatcher;
        _enemyDispatcher = enemyDispatcher;
        _shieldDispatcher = shieldDispatcher;
    }

    public override void Start()
    {
        _laserDispatcher.ActivateDispatcher(false);
        _enemyDispatcher.ActivateDispatcher(false);
    }

    public override void Update(GameTime gameTime)
    {
        ObserveLaserDispatcher();
        ObserveBonusesDispatcher();
        ObserverBackgroundStarsDispatcher(gameTime);
        ObserveEffectDispatcher();
        ObserveEnemyDispatcher(gameTime);
        ObserveShieldDispatcher();
    }

    private void ObserveLaserDispatcher() => _laserDispatcher.Observe();
    private void ObserveBonusesDispatcher() => _bonusesDispatcher.Observe();
    private void ObserveShieldDispatcher() => _shieldDispatcher.Observe();
    private void ObserveEffectDispatcher() => _effectDispatcher.Observe();
    private void ObserverBackgroundStarsDispatcher(GameTime gameTime)
    {
        _backgroundStarsDispatcher.Observe();
        _backgroundStarsDispatcher.SpawnBackgroundStar(gameTime);
    }
    private void ObserveEnemyDispatcher(GameTime gameTime)
    {
        _enemyDispatcher.Observe();
        _enemyDispatcher.SpawnEnemy(gameTime);
        _enemyDispatcher.UpdateBonusesDispatcher();
    }

}