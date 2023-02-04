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

    // Values
    [SerializeField] private float dmgInterval = 0.1f;
    [SerializeField] private const float range = 15f;

    private bool canDamage = true;

    private void Awake()
    {
        weapon = GetComponentInParent<LaserWeapon>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        // Raycast
        Vector2 fireDir = (weapon.aimPos - (Vector2)weapon.FirePoint.position).normalized;
        Vector2 endPos = (Vector2)weapon.FirePoint.position + (fireDir * range);

        RaycastHit2D[] hit = Physics2D.LinecastAll(weapon.FirePoint.position, endPos);
        
        if (hit.Length > 0)
        {
            Vector2 wallHit = Vector2.zero;
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].transform.gameObject.CompareTag("Enemy"))
                {
                    Enemy enemy = hit[i].transform.GetComponent<Enemy>();
                    enemy.TakeDamage(damage);
                    
                    canDamage = false;
                }
                else if(hit[i].transform.gameObject.CompareTag("Wall"))
                {
                    wallHit = hit[i].point;
                }
            }
            if (wallHit != Vector2.zero)
            {
                RepositionLine(weapon.FirePoint.position, wallHit);
            }
            else
                RepositionLine(weapon.FirePoint.position, endPos);
        }
        else
            RepositionLine(weapon.FirePoint.position, endPos);
    }

    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(dmgInterval);
        canDamage = true;
    }

    public void RepositionLine(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

}
