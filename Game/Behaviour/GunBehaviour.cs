public interface IShootPointsProvider
{
    GunData GunData {get;}
}

public sealed class GunBehaviour : Component, IShootPointsProvider
{
    public GunData GunData {get;}

    public GunBehaviour(GunData gunData)
    {
        GunData = gunData;
    }
}
