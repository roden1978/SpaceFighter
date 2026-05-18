using System;
using Microsoft.Xna.Framework;
public sealed class Asteroid : Enemy
{
    private float _distance;
    private float _startTime;
    private Vector2 _startPosition;
    private Random _random;
    private float _rotationSpeed;
    public Asteroid()
    {
        _random = new Random();
    }

    public override void Update(GameTime gameTime)
    {
        if (false == EnemyData.Ready)
            InitializeMoving(gameTime);
        else
            SmoothMove(gameTime);

        Rotation(gameTime);
    }

    private void InitializeMoving(GameTime gameTime)
    {
        _startPosition = gameObject.Transform.Position;
        _startTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
        _distance = Vector2.Distance(_startPosition, EnemyData.Destination);
        _rotationSpeed = _random.Next(5, 30);
        EnemyData.Ready = true;
    }

    private void SmoothMove(GameTime gameTime)
    {
        float distCovered = ((float)gameTime.TotalGameTime.TotalMilliseconds - _startTime) * EnemyData.Speed;

        float fractionOfJourney = distCovered / _distance;

        Vector2 position = Vector2.Lerp(_startPosition, EnemyData.Destination, fractionOfJourney);

        gameObject.Transform.Position = position;

    }

    private void Rotation(GameTime gameTime)
    {
        gameObject.Transform.Rotation = MathHelper.ToRadians((float)gameTime.TotalGameTime.TotalMilliseconds / _rotationSpeed);
    }

    public override void TakeDamage(float damage)
    {
        EnemyData.Health -= damage;
        if (EnemyData.Health <= 0)
        {
            EnemyData.ToPool = true;
            EnemyData.IsDestroed = true;
        }
    }

    public override void ResetData()
    {
        EnemyData.Health = AsteroidStaticData.Health;
        base.ResetData();
    }
}
