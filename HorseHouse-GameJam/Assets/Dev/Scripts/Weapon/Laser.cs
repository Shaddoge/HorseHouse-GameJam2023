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
    [SerializeField] private const float range = 15f;

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

        RaycastHit2D hit = Physics2D.Linecast(weapon.FirePoint.position, endPos);
        Debug.Log(weapon.transform.position);
        if (hit.collider != null)
        {
            RepositionLine(weapon.FirePoint.position, hit.point);
        }
        else
            RepositionLine(weapon.FirePoint.position, endPos);
    }

    public void RepositionLine(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

}
