using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : Projectile
{
    // References
    private LaserWeapon weapon;
    private LineRenderer lineRenderer;
    [SerializeField] private GameObject wallHitVFX;

    // Values
    [SerializeField] private float dmgInterval = 0.1f;
    [SerializeField] private const float range = 15f;

    private Coroutine damageCD = null;

    private bool canDamage = true;

    private void Awake()
    {
        weapon = GetComponentInParent<LaserWeapon>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnDisable()
    {
        if (damageCD != null)
        {
            StopCoroutine(damageCD);
        }
    }

    private void Update()
    {
        if (!lineRenderer.enabled) return;
        Vector2 fireDir = (weapon.aimPos - (Vector2)weapon.FirePoint.position).normalized;
        Vector2 endPos = (Vector2)weapon.FirePoint.position + (fireDir * range);
        // Linecast
        RaycastHit2D[] hit = Physics2D.LinecastAll(weapon.FirePoint.position, endPos);
        
        if (hit.Length > 0)
        {
            Vector2 wallHit = Vector2.zero;
            bool enemyHit = false;
            // Check for walls first before damaging enemies
            for (int i = 0; i < hit.Length; i++)
            {
                if(hit[i].transform.gameObject.CompareTag("Wall"))
                {
                    wallHit = hit[i].point;
                }
            }

            // Cut the line and recalculate linecast
            if (wallHit != Vector2.zero)
            {
                RepositionLine(weapon.FirePoint.position, wallHit);
                // Linecast
                hit = Physics2D.LinecastAll(weapon.FirePoint.position, wallHit);
            }
            else
            {
                RepositionLine(weapon.FirePoint.position, endPos);
            }

            for (int i = 0; i < hit.Length; i++)
            {
                if (canDamage && hit[i].transform.gameObject.CompareTag("Enemy"))
                {
                    Enemy enemy = hit[i].transform.GetComponent<Enemy>();
                    enemy.TakeDamage(damage);
                    enemyHit = true;
                }
            }
            // Damage cooldown
            if (enemyHit)
            {
                canDamage = false;
                if (damageCD == null)
                    damageCD = StartCoroutine(DamageCooldown());
            }
        }
        else
        {
            RepositionLine(weapon.FirePoint.position, endPos);
        }
        wallHitVFX.transform.position = lineRenderer.GetPosition(1);
    }

    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(dmgInterval);
        canDamage = true;
        damageCD = null;
    }

    public void RepositionLine(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    public void ToggleLaser(bool flag)
    {
        lineRenderer.enabled = flag;
        wallHitVFX.SetActive(flag);
    }
}
