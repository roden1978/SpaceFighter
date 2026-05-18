using Microsoft.Xna.Framework;

public class LaserData
{
    public LasersTypes LasersType;
    public float Speed = .7f;
    public float Damage = .1f;
    public Vector2 Destination = Vector2.Zero;
    public bool ToPool;
    public bool IsReady;
    public bool IsCreateEffect;

    public override string ToString() => $"Type: {LasersType}, Speed: {Speed}, Damage: {Damage}, ToPool: {ToPool}, IsReady: {IsReady}, isCreateEffect {IsCreateEffect}";
    public void ResetData()
    {
        LasersType = LasersTypes.None;
        Destination = Vector2.Zero;
        ToPool = false;
        IsReady = false;
        IsCreateEffect = false;
        Destination = Vector2.Zero;
    }
}