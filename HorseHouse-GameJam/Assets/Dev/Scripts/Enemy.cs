using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] Player player;
    [SerializeField] Rigidbody2D rigidBody;

    enum State { Move, Attack};
    State currState = State.Move;

    float ticks = 0.0f;
    const float INTERVAL = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rigidBody = this.GetComponent<Rigidbody2D>(); //Use rb from parent
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState) 
        {
            case State.Move: Move(); break;
            case State.Attack: Attack(); break;
        }
    }

    void Move()
    {
        Vector2 playerDir = player.transform.position - this.transform.position;
        moveDir.x = playerDir.x * 0.2f; //Change to move speed
        moveDir.y = playerDir.y * 0.2f;
        float angle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;
        this.rigidBody.rotation = angle;
        ticks = 0.0f;
    }

    void Attack()
    {
        ticks += Time.deltaTime;
        if(ticks >= INTERVAL)
        {
            Debug.Log("Attack Player");
            ticks = 0.0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collide");
        if(collision.gameObject.tag == "Player")
        {
            currState = State.Attack;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            currState = State.Attack;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            currState = State.Move;
        }
    }

    public void TakeDamage(int damage) 
    {
       
    }

    void Die()
    {
        Destroy(this); 
    }
}
