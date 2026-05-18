public class PickeableData
{
    public PickeableTypes PickeableType;
    public LasersTypes WeaponsType;
    public bool ToPool;
    public int Price;
    public bool IsPickedUp;
    public bool IsDestroed;

    public override string ToString()
    {
        return $"Goods: {PickeableType} weapon: {WeaponsType} toPool {ToPool}";
    }
    public void ResetData()
    {
        PickeableType = PickeableStaticData.PickeableType;
        WeaponsType = PickeableStaticData.WeaponsType;
        ToPool = PickeableStaticData.ToPool;
        IsPickedUp = PickeableStaticData.IsPickedUp;
        IsDestroed = PickeableStaticData.IsDestroed;
    }
}

public static class PickeableStaticData
{
    public static PickeableTypes PickeableType { get;} = PickeableTypes.None;
    public static LasersTypes WeaponsType { get;} = LasersTypes.None;
    public static bool ToPool {get;} = false;
    public static int Price {get;} = 20;
    public static bool IsPickedUp {get;} = false;
    public static bool IsDestroed {get;} = false;
}
