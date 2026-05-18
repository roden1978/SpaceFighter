using System;
using Microsoft.Xna.Framework;
public sealed class ShipModel : IShipHealthProvider, IShipEnergyProvider, IShooteable
{
    public event Action ShipDeath;
    public IFloatValueProvider Health { get => _shipData.Health; set => _shipData.Health.Value = value.Value; }
    public IFloatValueProvider Energy { get => _shipData.Energy; set => _shipData.Energy.Value = value.Value; }
    private readonly IInputService _input;
    private readonly IEffectDispatcher _effectDispatcher;
    private readonly ShipData _shipData;
    private Vector2 _shipCurrentPosition;
    private bool _isCanMove = false;

    public ShipModel(IInputService input, IEffectDispatcher effectDispatcher)
    {
        _input = input;
        _effectDispatcher = effectDispatcher;
        _shipData = new();
    }

    private Vector2 GetDirection() => _input.Move();

    public Vector2 ShipMove(GameTime gameTime, Vector2 currentPosition)
    {
        if (false == _isCanMove) return _shipData.StartPosition;

        Vector2 direction = GetDirection();

        int deltaTime = gameTime.ElapsedGameTime.Milliseconds;
        Vector2 delta = new(direction.X * deltaTime * _shipData.Speed, direction.Y * deltaTime * _shipData.Speed);
        Vector2 newPosition = Vector2.Clamp(currentPosition += delta, new(-Settings.ScreenWidth / 2, -Settings.ScreenHeight / 2), new Vector2(Settings.ScreenWidth / 2, Settings.ScreenHeight / 2));
        _shipCurrentPosition = newPosition;
        return newPosition;
    }

    public void TakeDamage(float damage)
    {
        _shipData.Health.Value -= damage;

        if (_shipData.Health.Value <= 0)
        {
            _shipData.ResetData();
            ShipDeath?.Invoke();
            _effectDispatcher.CreateEffect(EffectType.ShipExplosion, _shipCurrentPosition);
        }
    }

    public void PowerUp(float value = 0)
    {
        value = value == 0 ? _shipData.PowerUpValue : value;
        _shipData.Energy.Value = MathHelper.Clamp(_shipData.Energy.Value += value, 0, 1);
    }

    public void HealthUp(float value = 0)
    {
        _shipData.Health.Value = MathHelper.Clamp(_shipData.Health.Value += value, 0, 1);
    }

    public bool TryShoot(float power)
    {
        if (_shipData.Energy.Value > 0 & _shipData.Energy.Value >= power)
        {
            _shipData.Energy.Value -= power;
            return true;
        }

        return false;
    }

    public void ApplyDamage(IDamageable damageable)
    {
        damageable.TakeDamage(_shipData.Damage);
    }

    public Vector2 GetStartPosition()
    {
        return _shipData.StartPosition;
    }

    public void ActivateShip(bool value)
    {
        _isCanMove = value;
    }
}