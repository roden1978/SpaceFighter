public class DispatchersObserverFactory : IFactory<GameObject>
{
    private readonly DispatchersObserver _dispatchersObserver;

    public string Name => GetType().Name;

    public DispatchersObserverFactory(DispatchersObserver dispatchersObserver)
    {
        _dispatchersObserver = dispatchersObserver;
    }
    public GameObject Create()
    {
        GameObject observerGameObject = new($"DispatchersObserver");
        observerGameObject.AddComponent(_dispatchersObserver);

        return observerGameObject;
    }
}

