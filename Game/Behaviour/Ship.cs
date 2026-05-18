using Microsoft.Xna.Framework;
public interface IShipActivator
{
    bool Active {get;}
    void SetActive(bool value);
}

public sealed class Ship : Component, IPositionAdapter, IGunProvider, IDamageable, IPickeableCollector, IShipActivator
{
    public Vector2 Position { get => gameObject.Transform.Position; set => gameObject.Transform.Position = value; }
    public IShootPointsProvider Gun { get => _gun; set => _gun = value; }
    private const float Full = 1f;
    private readonly Vector2 _healthBoosterPosition = new(30, 70);
    private readonly Vector2 _energyBoosterPosition = new(-30, 70);
    private readonly ShipModel _shipModel;
    private IShootPointsProvider _gun;
    private GunsController _gunController;
    private readonly IShieldDispatcher _shieldDispatcher;
    private readonly IEffectDispatcher _effectDispatcher;

    public Ship(ShipModel shipModel, IShieldDispatcher shieldDispatcher, IEffectDispatcher effectDispatcher)
    {
        _shipModel = shipModel;
        _shieldDispatcher = shieldDispatcher;
        _effectDispatcher = effectDispatcher;
    }

    public override void Start()
    {
        _gunController = gameObject.GetComponent<GunsController>();
    }

    public override void Update(GameTime gameTime)
    {
        Move(gameTime);
        PowerUp();
    }

    private void Move(GameTime gameTime) => gameObject.Transform.Position = _shipModel.ShipMove(gameTime, currentPosition: Position);

    private void PowerUp() => _shipModel.PowerUp();

    public void TakeDamage(float damage) => _shipModel.TakeDamage(damage);

    public void PickUp(PickeableData pickeableData)
    {
        switch (pickeableData.PickeableType)
        {
            case PickeableTypes.EnergyPill:
                _shipModel.PowerUp(Full);
                _effectDispatcher.CreateEffect(EffectType.EnergyBooster, _energyBoosterPosition, gameObject.Transform);
                break;
            case PickeableTypes.HealthPill:
                _shipModel.HealthUp(Full);
                _effectDispatcher.CreateEffect(EffectType.HealthBooster, _healthBoosterPosition, gameObject.Transform);
                break;
            case PickeableTypes.ShieldUp:
                _shieldDispatcher.ActivateShield(gameObject.Transform);
                break;
            case PickeableTypes.Weapon:
                switch (pickeableData.WeaponsType)
                {
                    case LasersTypes.RedLaser:
                        _gunController.SwitchWeapon(LasersTypes.RedLaser);
                        _shipModel.PowerUp(Full);
                        _effectDispatcher.CreateEffect(EffectType.EnergyBooster, _energyBoosterPosition, gameObject.Transform);
                        break;
                    case LasersTypes.BlueLaser:
                        _gunController.SwitchWeapon(LasersTypes.BlueLaser);
                        _shipModel.PowerUp(Full);
                        _effectDispatcher.CreateEffect(EffectType.EnergyBooster, _energyBoosterPosition, gameObject.Transform);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }

    public override void OnCollisionEnter(BoxCollider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out IDamageable damageable))
            _shipModel.ApplyDamage(damageable);
    }

    public void ResetPosition()
    {
        Position = ShipStaticData.StartPosition;
    }

    void IShipActivator.SetActive(bool value)
    {
        gameObject.SetActive(value);
        if(value) ResetGun();
    }
    
    public void ResetGun()
    {
        _gunController.ResetGun();
    }

}
