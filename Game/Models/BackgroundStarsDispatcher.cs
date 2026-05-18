using System;
using Microsoft.Xna.Framework;

public interface IBackgroundStarsDispatcher
{
    void Observe();
    void SpawnBackgroundStar(GameTime gameTime);
}
public sealed class BackgroundStarsDispatcher : Dispatcher<BackgroundStar>, IBackgroundStarsDispatcher
{
    private readonly PoolService _poolService;
    private readonly Random _random;
    private const string Star = "BackgroundStar";
    private double _currentDelay;
    public BackgroundStarsDispatcher(PoolService poolService)
    {
        _poolService = poolService;
        _random = new Random();
    }

    public void SpawnBackgroundStar(GameTime gameTime)
    {

        if (_currentDelay < Settings.BackgroundStarsDelay)
        {
            _currentDelay += gameTime.ElapsedGameTime.TotalSeconds;
        }
        else
        {
            GameObject newGameObject = _poolService.GetPooledObject(Star);
            BackgroundStar backgroundStar = newGameObject.GetComponent<BackgroundStar>();
            ConstructBackgroundStar(backgroundStar);
            AddToObserve(backgroundStar);

            _currentDelay = 0;
        };

    }

    private void ConstructBackgroundStar(BackgroundStar backgroundStar)
    {
        backgroundStar.Position = new(_random.Next(-Settings.ScreenWidth / 2, Settings.ScreenWidth / 2), Settings.ScreenHeight / 2);
        backgroundStar.BackgroundStarData.Destination = new(backgroundStar.Position.X, -Settings.ScreenHeight / 2);
    }

    protected override void ReturnToPool(BackgroundStar useles)
    {
        PoolService.ReturnToPool(useles.gameObject);
        useles.ResetData();
    }

    protected override void Control(BackgroundStar observable)
    {

        if (observable.BackgroundStarData.ToPool)
            {
                AddToUseles(observable);
                return;
            }
        
        AddToUseful(observable);
    }
}