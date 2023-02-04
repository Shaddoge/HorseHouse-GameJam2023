using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sprite))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    // References
    private Sprite sprite;
    private Rigidbody2D rigidBody;

    // Stats
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    [SerializeField] private float moveSpeed;

    // Movement
    protected Vector2 moveDir;

    private void Awake()
    {
        sprite = this.GetComponent<Sprite>();
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
    private void FixedUpdate()
    {
        Vector2 newPos = rigidBody.position + (moveDir * moveSpeed * Time.fixedDeltaTime);
        rigidBody.MovePosition(newPos);
    }

    public void TakeDamage(int damage)
    {
        this.GetComponent<SpriteRenderer>().color = new Color(171 / 255.0f, 45 / 255.0f, 36 / 255.0f);
        StartCoroutine(ResetSpriteColor());
        this.health -= damage;
        if (this.health <= 0)
        {
            this.health = 0;

            Die();
        }
    }

    IEnumerator ResetSpriteColor()
    {
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().color = new Color(245 / 255.0f, 119 / 255.0f, 110 / 255.0f);
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }

}
