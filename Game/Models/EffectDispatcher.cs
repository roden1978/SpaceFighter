using System;
using Microsoft.Xna.Framework;

public interface IEffectDispatcher
{
    event Action GameOver;
    void Observe();
    Effect CreateEffect(EffectType effectType, Vector2 at, Transform2D parent = null);
}
public class EffectDispatcher : Dispatcher<Effect>, IEffectDispatcher
{
    public event Action GameOver;
    private readonly PoolService _poolService;

    public EffectDispatcher(PoolService poolService)
    {
        _poolService = poolService;
    }

    public Effect CreateEffect(EffectType effectType, Vector2 at, Transform2D parent = null)
    {
        GameObject effectGameObject = _poolService.GetPooledObject(effectType.ToString());
        Effect effect = effectGameObject.GetComponent<Effect>();
        
        if (parent != null)
            effectGameObject.Transform.Parent = parent;

        effect.Position = at;
        effect.EffectData.EffectType = effectType;
        AddToObserve(effect);

        return effect;
    }

    protected override void AdditionalActions(Effect effect)
    {
        if(effect.EffectData.EffectType == EffectType.ShipExplosion)
        {
            GameOver?.Invoke();
        }
    }

    protected override void ReturnToPool(Effect useles)
    {
        useles.ResetData();
        PoolService.ReturnToPool(useles.gameObject);
    }

    protected override void Control(Effect observable)
    {
        if (observable.EffectData.ToPool)
        {
            AddToUseles(observable);
            return;
        }

        AddToUseful(observable);
    }
}
