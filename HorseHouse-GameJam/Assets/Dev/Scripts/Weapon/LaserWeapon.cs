using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : RangedWeapon
{
    // Laser instance
    [SerializeField] private Laser laser;

    public override void StartFire()
    {
        base.StartFire();
        laser.gameObject.SetActive(true);
    }
    public override void StopFire()
    {
        base.StopFire();
        laser.gameObject.SetActive(false);
    }

    public override void OnUpdate()
    {
        if (!isFiring) return;
        RepositionLaser();
    }

    private void RepositionLaser()
    {
        
    }
}
