using Microsoft.Xna.Framework;

public static class ShipStaticData
{
    public static float Speed => .45f;
    public static float PowerUpValue => 0.0005f;
    public static float Damage => 1f;
    public static float Health => 1f;
    public static float Energy => 1f;
    public static Vector2 StartPosition => new(0, -Settings.ScreenHeight / 10);
}
