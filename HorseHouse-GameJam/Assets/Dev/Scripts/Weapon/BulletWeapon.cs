using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BulletWeapon : RangedWeapon
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private ParticleSystem muzzleVFX;
    [SerializeField] private ParticleSystem smokeVFX;
    [SerializeField] private AudioClip fireSFX;

    private AudioSource source;

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
        if (muzzleVFX != null)
        {
            muzzleVFX.Emit(12);
        }
        if (smokeVFX != null)
        {
            smokeVFX.Emit(5);
        }

        AudioManager.Instance.PlaySFX(fireSFX, UnityEngine.Random.Range(0.3f, 0.7f));
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (!canFire && cdTicks < fireRate)
        {
            cdTicks += Time.deltaTime;
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
