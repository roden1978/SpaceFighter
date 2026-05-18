using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public sealed class Ufo : Enemy, IShootPointsProvider
{
    private float _distance;
    private float _startTime;
    private Vector2 _startPosition;
    private float _currentDelay;
    private readonly ILaserDispatcher _laserDispatcher;
    private readonly IPositionAdapter _target;
    public GunData GunData {get; private set;}
    public IPositionAdapter UfoPositionAdapter => this;

    public Ufo(ILaserDispatcher laserDispatcher, IPositionAdapter target)
    {
        _laserDispatcher = laserDispatcher;
        _target = target;
    }
    public override void Start()
    {
        GunData = new GunData([gameObject.Transform], LasersTypes.EnemyLaser, 0, 0);
    }

    public override void Update(GameTime gameTime)
    {
        Shoot(gameTime);

        if (false == EnemyData.Ready)
            InitializeMoving(gameTime);
        else
            SmoothMove(gameTime);
    }

    private void InitializeMoving(GameTime gameTime)
    {
        _startPosition = gameObject.Transform.Position;
        _startTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
        _distance = Vector2.Distance(_startPosition, EnemyData.Destination);
        EnemyData.Ready = true;
    }

    private void Shoot(GameTime gameTime)
    {
        
        if (_currentDelay < EnemyData.ShootDelay)
            _currentDelay += (float)gameTime.ElapsedGameTime.TotalSeconds;
        else
        {
            IEnumerable<Laser> lasers = _laserDispatcher.Shoot(this);
            foreach (Laser laser in lasers)
            {
                laser.LaserData.Destination = _target.Position;
                Vector2 direction = _target.Position - gameObject.Transform.Position;
                float radians = MathF.Atan2(direction.Y, direction.X);
                laser.gameObject.Transform.Rotation = radians - MathHelper.PiOver2;
            }
            _currentDelay = 0;
        }
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
        //gameObject.Transform.Rotation = MathHelper.ToRadians((float)gameTime.TotalGameTime.TotalMilliseconds / _rotationSpeed);
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
        EnemyData.Health = UfoStaticData.Health;
        base.ResetData();
    }
}