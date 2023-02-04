using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeapon : RangedWeapon
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private float fireRate = 0.2f;
    private float cdTicks = 0.0f;
    private bool canFire = true;
    
    public override void StartFire()
    {
        base.StartFire();
        if (canFire)
        {
            Shoot();
        }
    }
    public override void StopFire()
    {
        base.StopFire();
    }

    private void Shoot()
    {
        if (!canFire) return;
        canFire = false;
        StartCoroutine(SimulateBullet());
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (!canFire && cdTicks < fireRate)
        {
            cdTicks += Time.deltaTime;
            Debug.Log(cdTicks);
            if (cdTicks >= fireRate)
            {
                canFire = true;
                cdTicks = 0.0f;
                if (isFiring)
                {
                    Shoot();
                }
            }
        }
    }

    private IEnumerator SimulateBullet()
    {
        bullet.ToggleLine(true);
        bullet.Fire();
        yield return new WaitForSeconds(0.02f);
        bullet.ToggleLine(false);
    }
}
