using System.Collections.Generic;

public abstract class Dispatcher<T> where T : Component
{
    protected bool Active = true;
    protected IReadOnlyList<T> Observables => _observables;
    private readonly List<T> _observables = new(16);
    private readonly List<T> _usefules = new(16);
    private readonly List<T> _useleses = new(16);

    public void Observe()
    {
        if (false == Active) return;

        foreach (T observable in _observables)
            Control(observable);

        _observables.Clear();

        foreach (T useful in _usefules)
            _observables.Add(useful);

        _usefules.Clear();

        foreach (T useles in _useleses)
            LifeTimeComplition(useles);

        _useleses.Clear();
    }

    protected abstract void ReturnToPool(T useles);
    protected abstract void Control(T observable);
    private void LifeTimeComplition(T useles)
    {
        AdditionalActions(useles);
        ReturnToPool(useles);
    }

    protected void AddToUseful(T observable) => _usefules.Add(observable);
    protected void AddToUseles(T observable) => _useleses.Add(observable);
    protected void AddToObserve(T observable) => _observables.Add(observable);
    protected virtual void AdditionalActions(T observable) { }
    public virtual void ActivateDispatcher(bool value)
    {
        if (value == false)
            Cleanup();

        Active = value;
    }
    private void Cleanup()
    {
        _observables.ForEach(ReturnToPool);
        _observables.Clear();
        _usefules.Clear();
        _useleses.Clear();
    }
}