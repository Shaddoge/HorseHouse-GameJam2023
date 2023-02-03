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
    [SerializeField] private int health;
    [SerializeField] private float moveSpeed;

    // Movement
    protected Vector2 moveDir;

    private void Awake()
    {
        sprite = this.GetComponent<Sprite>();
        rigidBody = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 newPos = rigidBody.position + (moveDir * moveSpeed * Time.fixedDeltaTime);
        rigidBody.MovePosition(newPos);
    }

}
