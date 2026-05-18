using System;
using Microsoft.Xna.Framework;

public sealed class BackgroundStar : Component, IPositionAdapter
{
    public BackgroundStarData BackgroundStarData => _data;
    public Vector2 Position { get => gameObject.Transform.Position; set => gameObject.Transform.Position = value; }
    private BackgroundStarData _data;
    private float _distance;
    private float _startTime;
    private Vector2 _startPosition;
    private Random _random;
    public BackgroundStar()
    {
        _data = new BackgroundStarData();
        _random = new Random();
    }

    public override void Update(GameTime gameTime)
    {
        if (false == _data.Ready)
            InitializeMoving(gameTime);
        else
            SmoothMove(gameTime);
    }

    private void InitializeMoving(GameTime gameTime)
    {
        _startPosition = gameObject.Transform.Position;
        _startTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
        _distance = Vector2.Distance(_startPosition, _data.Destination);
        _data.Speed = Math.Clamp(_random.NextSingle(), 0, _data.Speed);
        _data.Ready = true;
    }

    private void SmoothMove(GameTime gameTime)
    {
        float distCovered = ((float)gameTime.TotalGameTime.TotalMilliseconds - _startTime) * _data.Speed;

        float fractionOfJourney = distCovered / _distance;

        Vector2 position = Vector2.Lerp(_startPosition, _data.Destination, fractionOfJourney);

        gameObject.Transform.Position = position;

        if (gameObject.Transform.Position.Y <= -Settings.ScreenHeight / 2)
            BackgroundStarData.ToPool = true;
    }

    public void ResetData()
    {
        _data.ResetData();
    }
}
