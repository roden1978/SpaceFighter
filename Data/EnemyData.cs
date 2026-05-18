using Microsoft.Xna.Framework;

public class EnemyData
{
    public float Health;
    public float Speed;
    public Vector2 Destination = Vector2.Zero;
    public bool Ready = false;
    public bool ToPool = false;
    public bool IsDestroed = false;
    public int Price;
    public float Damage;
    public float ShootDelay = float.MaxValue;
    public PickeableData DropStuffData = new()
    {
        PickeableType = PickeableStaticData.PickeableType,
        WeaponsType = PickeableStaticData.WeaponsType,
        ToPool = false,
        Price = PickeableStaticData.Price,
        IsPickedUp = false
    };

    public void ResetData()
    {
        Destination = Vector2.Zero;
        Ready = false;
        ToPool = false;
        IsDestroed = false;

        DropStuffData.PickeableType = PickeableStaticData.PickeableType;
        DropStuffData.WeaponsType = PickeableStaticData.WeaponsType;
        DropStuffData.ToPool = false;
        DropStuffData.Price = PickeableStaticData.Price;
        DropStuffData.IsPickedUp = false;
    }
}
