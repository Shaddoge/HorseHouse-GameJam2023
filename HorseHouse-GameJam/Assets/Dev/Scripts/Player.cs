using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Player : Character
{
    // References
    private RangedWeapon weapon = null;
    private Animator animator = null;

    // Values
    [HideInInspector] public Vector2 pointerPos = Vector3.zero;

    private void Start()
    {
        Debug.Log("Player Health" + this.health);
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<RangedWeapon>();
        animator = GetComponentInChildren<Animator>();
        if (weapon != null)
        {
            weapon.Setup(this);
        }
    }
    private void OnEnable()
    {
        EventManager.Instance.takeDamage += TakeDamage;
    }
    private void OnDisable()
    {
        EventManager.Instance.takeDamage -= TakeDamage;
    }
    // Update is called once per frame
    void Update()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        pointerPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 viewportPos = (Camera.main.ScreenToViewportPoint(mousePos) * 2.0f) - Vector3.one;
        animator.SetFloat("X", viewportPos.x); 
        animator.SetFloat("Y", viewportPos.y);

        transform.localScale = new Vector3(Mathf.Sign(viewportPos.x), 1, 1);

        if (weapon != null)
        {
            weapon.SetAimPos(pointerPos);
            if (Input.GetMouseButtonDown(0))
            {
                weapon.StartFire();
                animator.SetBool("IsAttacking", true);
            }
            if (Input.GetMouseButtonUp(0))
            {
                weapon.StopFire();
                animator.SetBool("IsAttacking", false);
            }
        }

        if (moveDir.x != 0 || moveDir.y != 0)
        {
            if (animator.GetBool("IsMoving")) return;
            animator.SetBool("IsMoving", true);
        }
        else if (animator.GetBool("IsMoving"))
        {
            animator.SetBool("IsMoving", false);
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        EventManager.Instance.UpdateHealth(health);
    }
    public override void Die()
    {
        Debug.Log("Player Dead");
        //base.Die();
    }

}
