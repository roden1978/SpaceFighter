using Microsoft.Xna.Framework;

public sealed class Magnet : Component
{
    private readonly IPositionAdapter _positionAdapter;
    private readonly VerticalAmplifier _verticalAmplifier;
    private bool _isMove;
    private const float MagnetDistance = 150 * 150;

    public Magnet(IPositionAdapter positionAdapter, VerticalAmplifier verticalAmplifier)
    {
        _positionAdapter = positionAdapter;
        _verticalAmplifier = verticalAmplifier;
    }

    public override void Update(GameTime gameTime) => Magneting(gameTime);

    private void Magneting(GameTime gameTime)
    {
        if (Vector2.DistanceSquared(gameObject.Transform.Position, _positionAdapter.Position) < MagnetDistance & false == _isMove)
        {
            _isMove = true;
            _verticalAmplifier.SetActive(false);
        }
        
        if (_isMove)
        {
            Vector2 direction = _positionAdapter.Position - gameObject.Transform.Position;
            direction.Normalize();
            gameObject.Transform.Position += direction * .5f * gameTime.ElapsedGameTime.Milliseconds;
        }
    }
    public override void Disable() => _isMove = false;
}