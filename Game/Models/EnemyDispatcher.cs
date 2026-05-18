using System;
using Microsoft.Xna.Framework;
public interface IEnemyDispatcher
{
    void Observe();
    void SpawnEnemy(GameTime gameTime);
    void UpdateBonusesDispatcher();
    void ActivateDispatcher(bool value);
}
public sealed class EnemyDispatcher : Dispatcher<Enemy>, IEnemyDispatcher
{
    private readonly PoolService _poolService;
    private readonly IScoreStringProvider _scoreStringProvider;
    private readonly IResultProvider _resultProvider;
    private readonly IBonusesDispatcher _bonusesDispatcher;
    private readonly IEffectDispatcher _effectDispatcher;
    private readonly Random _random;
    private const string AsteroidName = "Asteroid";
    private const string UfoName = "Ufo";
    private double _currentDelay;
    private float _delay;

    public EnemyDispatcher(PoolService poolService,
        IScoreStringProvider scoreStringProvider,
        IResultProvider resultProvider,
        IBonusesDispatcher bonusesDispatcher,
        IEffectDispatcher effectDispatcher)
    {
        _poolService = poolService;
        _scoreStringProvider = scoreStringProvider;
        _resultProvider = resultProvider;
        _bonusesDispatcher = bonusesDispatcher;
        _effectDispatcher = effectDispatcher;
        _random = new Random();
        _delay = Settings.Delay;
    }
    public void UpdateBonusesDispatcher() => _bonusesDispatcher.Observe();

    public void SpawnEnemy(GameTime gameTime)
    {
        if (false == Active) return;

        if (_currentDelay < _delay)
        {
            _currentDelay += gameTime.ElapsedGameTime.TotalSeconds;
        }
        else
        {
            Enemy enemy;

            if (_random.Next(1, 100) > 50)
            {
                GameObject newGameObject = _poolService.GetPooledObject(AsteroidName);
                enemy = newGameObject.GetComponent<Asteroid>();
            }
            else
            {
                GameObject newGameObject = _poolService.GetPooledObject(UfoName);
                enemy = newGameObject.GetComponent<Ufo>();
            }

            ConstructEnemy(enemy);
            AddToObserve(enemy);
            _currentDelay = 0;
        }
    }

    public override void ActivateDispatcher(bool value)
    {
        if(false == value)
            RestoreSpawnDelay();

        base.ActivateDispatcher(value);
    }

    public void DecreaseDelaySpawnEnemy(float decreaseValue)
    {
        if(_delay <= 0.5f) return;

        _delay -= decreaseValue;
    }

    public void RestoreSpawnDelay()
    {
        _delay = Settings.Delay;
        _currentDelay = 0;
    }

    private void ConstructEnemy(Enemy enemy)
    {
        switch (enemy)
        {
            case Ufo:
                enemy.Position = new(_random.Next(-Settings.ScreenWidth / 2, Settings.ScreenWidth / 2), Settings.ScreenHeight / 2);

                int delta = _random.Next(Settings.ScreenWidth / 2);
                int newDelta = enemy.Position.X > 0 ? -delta : delta;
                enemy.EnemyData.Destination = new(enemy.Position.X
                    + newDelta,
                    -Settings.ScreenHeight / 2);

                enemy.EnemyData.Speed = _random.Next(1, 4) * .1f;
                float weaponChance = _random.NextSingle();

                enemy.EnemyData.DropStuffData.WeaponsType = weaponChance > .9f ? LasersTypes.BlueLaser
                : weaponChance > .8f ? LasersTypes.RedLaser : LasersTypes.None;

                enemy.EnemyData.DropStuffData.PickeableType = weaponChance > .95f ? PickeableTypes.ShieldUp : weaponChance > .7f ? PickeableTypes.Alien : PickeableTypes.None;
                break;
            case Asteroid:
                enemy.Position = new(_random.Next(-Settings.ScreenWidth / 2, Settings.ScreenWidth / 2), Settings.ScreenHeight / 2);

                enemy.EnemyData.Destination = new(enemy.Position.X
                    + _random.Next(-Settings.ScreenWidth / 2, Settings.ScreenWidth / 2),
                    -Settings.ScreenHeight / 2);
                enemy.EnemyData.Speed = _random.Next(1, 4) * .1f;
                float goodsChance = _random.NextSingle();
                enemy.EnemyData.DropStuffData.PickeableType = goodsChance > .75f ? PickeableTypes.HealthPill
                : goodsChance > .55f ? PickeableTypes.EnergyPill
                : PickeableTypes.None;
                break;
            default:
                break;
        }

    }

    protected override void AdditionalActions(Enemy useles)
    {
        if (false == useles.EnemyData.IsDestroed) return;

        _resultProvider.Result.Value += useles.EnemyData.Price;

        if(_resultProvider.Result.Value % Settings.UpDifficultScoresValue == 0)
            DecreaseDelaySpawnEnemy(Settings.DifficultUpValue);

        _scoreStringProvider.StringSource.Value = _resultProvider.Result.Value.ToString();
        _effectDispatcher.CreateEffect(EffectType.Explosion, useles.gameObject.Transform.AbsolutePosition);
    }

    protected override void ReturnToPool(Enemy useles)
    {
        Vector2 position = useles.gameObject.Transform.AbsolutePosition;
        PoolService.ReturnToPool(useles.gameObject);
        
        if (useles.EnemyData.IsDestroed)
            _bonusesDispatcher.CreateBonus(useles.EnemyData.DropStuffData, position);
        
        useles.ResetData();
    }

    protected override void Control(Enemy observable)
    {
        if (observable.Position.Y > -Settings.ScreenHeight / 2)
        {
            if (observable.EnemyData.ToPool)
            {
                AddToUseles(observable);
                return;
            }

            AddToUseful(observable);
        }
        else
        {
            AddToUseles(observable);
        }
    }
}
