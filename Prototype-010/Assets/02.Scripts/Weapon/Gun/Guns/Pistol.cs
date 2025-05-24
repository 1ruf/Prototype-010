using UnityEngine;

public class Pistol : Gun
{
    public Pistol(GunSO gunData, LoadType loadType) : base(gunData, loadType)
    {
    }

    protected override void Fire()
    {
        base.Fire();
    }
}
