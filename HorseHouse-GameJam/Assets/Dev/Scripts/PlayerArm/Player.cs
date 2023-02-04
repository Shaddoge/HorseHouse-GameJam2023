using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Player : Character
{
    // References
    private RangedWeapon weapon = null;
    
    // Values
    public Vector2 pointerPos = Vector3.zero;

    private void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<RangedWeapon>();
        if (weapon != null)
        {
            weapon.Setup(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        pointerPos = Camera.main.ScreenToWorldPoint(mousePos);

        if (weapon != null)
        {
            weapon.SetAimPos(pointerPos);
            if (Input.GetMouseButtonDown(0))
            {
                weapon.StartFire();
            }
            if (Input.GetMouseButtonUp(0))
            {
                weapon.StopFire();
            }
        }
        
    }

}
