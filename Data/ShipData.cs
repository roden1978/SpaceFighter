using Microsoft.Xna.Framework;
public sealed class ShipData
{
    public float Speed {get; private set;}
    public float PowerUpValue;
    public IFloatValueProvider Health;
    public IFloatValueProvider Energy;
    public float Damage;
    public Vector2 StartPosition;

    public ShipData()
    {
        Speed = ShipStaticData.Speed;
        PowerUpValue = ShipStaticData.PowerUpValue;
        Health = new Health()
        {
            Value = ShipStaticData.Health
        };
        Energy = new Energy()
        {
            Value = ShipStaticData.Energy
        };
        Damage = ShipStaticData.Damage;
        StartPosition = ShipStaticData.StartPosition;
    }

    public void ResetData()
    {
        Health.Value = ShipStaticData.Health;
        Energy.Value = ShipStaticData.Energy;
        StartPosition = ShipStaticData.StartPosition;
    }
}