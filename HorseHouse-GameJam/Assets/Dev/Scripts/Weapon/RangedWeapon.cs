using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[Serializable]
public enum FireType
{
    Single, 
    Auto, // Laser, SMG/Rifle, etc.
}

[RequireComponent(typeof(Sprite))]
public class RangedWeapon : MonoBehaviour
{
    // Values
    [SerializeField] private FireType fireType = FireType.Single;

    protected bool isFiring = false;
    private Vector3 aimDirection = Vector2.zero;

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
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        aimDirection = transform.position - lookPos;
        Debug.Log(aimDirection);
        Debug.DrawRay(transform.position, aimDirection * 10, Color.red);
        OnUpdate();
    }

    public virtual void OnUpdate()
    {
        
    }

    public void OnDrawGizmos()
    {
        
    }
}
