using Microsoft.Xna.Framework;

public sealed class BackgroundStarData
{
    public float Speed = .4f;
    public Vector2 Destination = Vector2.Zero;
    public bool Ready;
    public bool ToPool;

    public void ResetData()
    {
        Destination = Vector2.Zero;
        Ready = false;
        ToPool = false;
    }
}