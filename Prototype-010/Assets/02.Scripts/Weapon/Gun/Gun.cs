using UnityEngine;

public class Gun : Weapon
{
    public LoadType loadType { get; set; }
    public GunSO gunData { get; set; }

    public Gun(GunSO gunData, LoadType loadType)
    {
        this.gunData = gunData;
        this.loadType = loadType;
    }
    protected virtual void Fire()
    { 
    }
}
public enum LoadType
{
    ClosedBolt,
    PumpAction,
    BoltAction,
    LeverAction,
    LMG
}