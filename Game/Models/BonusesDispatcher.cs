using System;
using Microsoft.Xna.Framework;

public interface IBonusesDispatcher
{
    void Observe();
    void CreateBonus(PickeableData pickeableData, Vector2 at);
    void ActivateDispatcher(bool value);
}
public sealed class BonusesDispatcher : Dispatcher<Pickeable>, IBonusesDispatcher
{
    private readonly PoolService _poolService;
    private readonly IEffectDispatcher _effectDispatcher;
    private readonly IScoreStringProvider _scoreStringProvider;
    private readonly IResultProvider _resultProvider;

    public BonusesDispatcher(PoolService poolService, IEffectDispatcher effectDispatcher, IScoreStringProvider scoreStringProvider, IResultProvider resultProvider)
    {
        _poolService = poolService;
        _effectDispatcher = effectDispatcher;
        _scoreStringProvider = scoreStringProvider;
        _resultProvider = resultProvider;
    }
    public void CreateBonus(PickeableData pickeableData, Vector2 at)
    {
        GameObject bonus = default;
        Pickeable pickeable;

        if (pickeableData.PickeableType != PickeableTypes.None & pickeableData.WeaponsType == LasersTypes.None)
        {
            bonus = _poolService.GetPooledObject(pickeableData.PickeableType.ToString());
            bonus.Transform.Position = at;

            if (bonus.TryGetComponent(out VerticalAmplifier amplifier))
                amplifier.UpdateStartPosition(at);

            pickeable = bonus.GetComponent<Pickeable>();
            pickeable.PickeableData.PickeableType = pickeableData.PickeableType;
            AddToObserve(pickeable);

            return;
        }

        if (pickeableData.PickeableType == PickeableTypes.ShieldUp)
        {
            bonus = _poolService.GetPooledObject(pickeableData.PickeableType.ToString());
            bonus.Transform.Position = at;

            if (bonus.TryGetComponent(out VerticalAmplifier amplifier))
                amplifier.UpdateStartPosition(at);

            pickeable = bonus.GetComponent<Pickeable>();
            pickeable.PickeableData.PickeableType = pickeableData.PickeableType;
            AddToObserve(pickeable);

            return;
        }

        if (pickeableData.WeaponsType != LasersTypes.None)
        {
            switch (pickeableData.WeaponsType)
            {
                case LasersTypes.RedLaser:
                    bonus = _poolService.GetPooledObject("RedGunUp");
                    break;
                case LasersTypes.BlueLaser:
                    bonus = _poolService.GetPooledObject("BlueGunUp");
                    break;
                default:
                    break;
            }
            bonus.Transform.Position = at;

            if (bonus.TryGetComponent(out VerticalAmplifier amplifier))
                amplifier.UpdateStartPosition(at);

            pickeable = bonus.GetComponent<Pickeable>();
            pickeable.PickeableData.WeaponsType = pickeableData.WeaponsType;
            pickeable.PickeableData.PickeableType = PickeableTypes.Weapon;
            AddToObserve(pickeable);
            return;
        }
    }

    protected override void AdditionalActions(Pickeable useles)
    {
        if (useles.PickeableData.PickeableType == PickeableTypes.Alien)
        {
            useles.PickeableData.IsDestroed = false;
            _effectDispatcher.CreateEffect(EffectType.Blood, useles.gameObject.Transform.AbsolutePosition);
        }

        if (useles.PickeableData.IsDestroed)
            _effectDispatcher.CreateEffect(EffectType.Burst, useles.gameObject.Transform.AbsolutePosition);

        if (useles.PickeableData.IsPickedUp)
        {
            _resultProvider.Result.Value += useles.PickeableData.Price;
            _scoreStringProvider.StringSource.Value = _resultProvider.Result.Value.ToString();
        }
    }

    protected override void ReturnToPool(Pickeable useles)
    {
        PoolService.ReturnToPool(useles.gameObject);
        useles.ResetData();
    }

    protected override void Control(Pickeable observable)
    {
        if (observable.PickeableData.ToPool || observable.gameObject.Transform.Position.Y >= Settings.ScreenHeight / 2)
        {
            AddToUseles(observable);
            return;
        }

        AddToUseful(observable);
    }
}
