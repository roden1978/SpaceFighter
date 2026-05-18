using Microsoft.Xna.Framework;
public abstract class Laser : Component, IPositionAdapter
{
    public LaserData LaserData { get; private set; } = new();
    private float _distance;
    private float _startTime;
    private Vector2 _startPosition;

    public Vector2 Position { get => gameObject.Transform.Position; set => gameObject.Transform.Position = value; }

    public override void Update(GameTime gameTime)
    {
        if (false == LaserData.IsReady)
            InitializeMoving(gameTime);
        else
            SmoothMove(gameTime);
    }

    private void InitializeMoving(GameTime gameTime)
    {
        _startPosition = gameObject.Transform.Position;
        _startTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
        _distance = Vector2.Distance(_startPosition, LaserData.Destination);
        LaserData.IsReady = true;
    }

    private void SmoothMove(GameTime gameTime)
    {
        float distCovered = ((float)gameTime.TotalGameTime.TotalMilliseconds - _startTime) * LaserData.Speed;

        float fractionOfJourney = distCovered / _distance;

        Vector2 position = Vector2.Lerp(_startPosition, LaserData.Destination, fractionOfJourney);

        gameObject.Transform.Position = position;
    }

    public override void OnCollisionEnter(BoxCollider2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(LaserData.Damage);
            LaserData.IsCreateEffect = true;
        }
        LaserData.ToPool = true;
    }
    public void ResetData()
    {
        LaserData.ResetData();
    }

}
