using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : RangedWeapon
{
    // Laser instance
    [SerializeField] private Laser laser;
    
    private bool canDamage = true;
    
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
        base.OnUpdate();
        if (!isFiring) return;
        //laser.RepositionLine((Vector2)firePoint.position, aimPos);
    }
}
