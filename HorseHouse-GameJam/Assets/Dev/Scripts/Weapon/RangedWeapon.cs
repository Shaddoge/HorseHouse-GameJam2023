using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum FireType
{
    Single, 
    Auto, // Laser, SMG/Rifle, etc.
}

[RequireComponent(typeof(SpriteRenderer))]
public class RangedWeapon : MonoBehaviour
{
    // References
    private Player player;
    // Values
    [SerializeField] private FireType fireType = FireType.Single;
    [SerializeField] protected Transform firePoint;
    public Transform FirePoint { get { return firePoint; } }

    protected bool isFiring = false;
    public Vector2 aimPos = Vector2.zero;

    private void Awake()
    {
        if (firePoint == null)
            firePoint = this.transform;
    }

    public void Setup(Player player)
    {
        this.player = player;
    }

    public virtual void StartFire()
    {
        isFiring = true;
    }

    public virtual void StopFire()
    {
        isFiring = false;
    }

    private void Update()
    {
        OnUpdate();
    }
    
    public virtual void OnUpdate()
    {
        if (player == null) return;
        transform.right = (player.pointerPos - (Vector2)transform.position).normalized;
    }

    public void SetAimPos(Vector2 pos)
    {
        aimPos = pos;
    }
}
