using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    // References
    private Rigidbody2D rigidBody;

    // Stats
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    [SerializeField] private float moveSpeed;

    // Movement
    protected Vector2 moveDir;

    private void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
    }
    public Rigidbody2D getRigidBody()
    {
        return this.rigidBody;
    }
    public float getMovespeed()
    {
        return this.moveSpeed;
    }

    public void setMovespeed(float speed)
    {
        this.moveSpeed = speed;
    }
    public virtual void FixedUpdate()
    {
        Vector2 newPos = rigidBody.position + (moveDir * moveSpeed * Time.fixedDeltaTime);
        rigidBody.MovePosition(newPos);
    }

    public virtual void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            this.health = 0;

            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }

}
