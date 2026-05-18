public interface IShieldDispatcher
{
    void Observe();
    void ActivateShield(Transform2D parent);
}
public sealed class ShieldDispatcher : Dispatcher<Shield>, IShieldDispatcher
{
    private PoolService _poolService;
    private const string Shield = "Shield";
    public ShieldDispatcher(PoolService poolService)
    {
        _poolService = poolService;
    }
    public void ActivateShield(Transform2D parent)
    {
        GameObject shieldGameObject = _poolService.GetPooledObject(Shield);
        shieldGameObject.Transform.Parent = parent;
        shieldGameObject.Transform.Position = new(0, 30);
        Shield shield = shieldGameObject.GetComponent<Shield>();

        foreach (Shield observable in Observables)
            observable.ShieldData.Health = 0;

        AddToObserve(shield);
    }
    protected override void ReturnToPool(Shield useles)
    {
        PoolService.ReturnToPool(useles.gameObject);
        useles.ResetData();
    }

    protected override void Control(Shield observable)
    {
        if (observable.ShieldData.Health > 0)
            AddToUseful(observable);
        else
            AddToUseles(observable);
    }
}
