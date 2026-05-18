public class GunData
{
    public Transform2D[] ShootPoints {get;}
    public LasersTypes LaserType {get;}
    public float Power {get;}
    public int Priority {get;}

    public GunData(Transform2D[] shootPoints, LasersTypes laserType, float power, int priority)
    {
        ShootPoints = shootPoints;
        LaserType = laserType;
        Power = power;
        Priority = priority;
    }
}



