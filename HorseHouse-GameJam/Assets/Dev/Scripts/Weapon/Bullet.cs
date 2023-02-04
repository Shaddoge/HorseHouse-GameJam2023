using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    // References
    private BulletWeapon weapon;
    private LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem wallHitVFX;

    // Values
    [SerializeField] private float range = 10f;

    private void Awake()
    {
        weapon = GetComponentInParent<BulletWeapon>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void Fire()
    {
        Vector2 fireDir = (weapon.aimPos - (Vector2)weapon.FirePoint.position).normalized;
        Vector2 endPos = (Vector2)weapon.FirePoint.position + (fireDir * range);
        // Detect hit
        RaycastHit2D hit = Physics2D.Linecast(weapon.FirePoint.position, endPos);
        if (hit.collider != null)
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
            }
            RepositionLine(weapon.FirePoint.position, hit.point);
            StartCoroutine(SimulateHitVFX(hit));
        }
        else
            RepositionLine(weapon.FirePoint.position, endPos);
    }

    public void RepositionLine(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    public void ToggleLine(bool flag)
    {
        lineRenderer.enabled = flag;
    }

    private IEnumerator SimulateHitVFX(RaycastHit2D hit)
    {
        wallHitVFX.transform.LookAt(hit.normal);
        wallHitVFX.transform.position = hit.point;
        wallHitVFX.Play();
        yield return new WaitForSeconds(0.05f);
        wallHitVFX.Stop();
}
}
