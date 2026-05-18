using System;
using System.Collections.Generic;
public interface ILaserDispatcher
{
    void Observe();
    IEnumerable<Laser> Shoot(IShootPointsProvider shootPointsProvider);
    void ActivateDispatcher(bool value);
}
public sealed class LaserDispatcher : Dispatcher<Laser>, ILaserDispatcher
{
    private readonly PoolService _poolService;
    private readonly IEffectDispatcher _effectDispatcher;

    public LaserDispatcher(PoolService poolService, IEffectDispatcher effectDispatcher)
    {
        _poolService = poolService;
        _effectDispatcher = effectDispatcher;
    }
    public IEnumerable<Laser> Shoot(IShootPointsProvider shootPointsProvider)
    {
        if (Active)
            for (int i = 0; i < shootPointsProvider.GunData.ShootPoints.Length; i++)
            {
                GameObject newLaser = _poolService.GetPooledObject(shootPointsProvider.GunData.LaserType.ToString());
                Laser laser = newLaser.GetComponent<Laser>();
                laser.Position = shootPointsProvider.GunData.ShootPoints[i].AbsolutePosition;
                laser.LaserData.LasersType = shootPointsProvider.GunData.LaserType;
                AddToObserve(laser);
                yield return laser;
            }
    }

    protected override void AdditionalActions (Laser useles)
    {
        if (useles.LaserData.IsCreateEffect)
            CreateLaserFlash(useles);
    }
    protected override void ReturnToPool(Laser useles)
    {
        PoolService.ReturnToPool(useles.gameObject);
        useles.ResetData();
    }

    protected override void Control(Laser observable)
    {
        if (observable.Position.Y < Settings.ScreenHeight / 2
            & observable.Position.Y > -Settings.ScreenHeight / 2
            & observable.Position.X < Settings.ScreenWidth / 2
            & observable.Position.X > -Settings.ScreenWidth / 2)
        {
            if (observable.LaserData.ToPool)
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

    private void CreateLaserFlash(Laser laser)
    {
        switch (laser.LaserData.LasersType)
        {
            case LasersTypes.GreenLaser:
                _effectDispatcher.CreateEffect(EffectType.GreenLaserFlash, laser.Position);
                break;
            case LasersTypes.RedLaser:
            case LasersTypes.EnemyLaser:
                _effectDispatcher.CreateEffect(EffectType.RedLaserFlash, laser.Position);
                break;
            case LasersTypes.BlueLaser:
                _effectDispatcher.CreateEffect(EffectType.BlueLaserFlash, laser.Position);
                break;
            default:
                break;
        }
    }

}