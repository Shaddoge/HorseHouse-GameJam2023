using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] Player player;
    enum State { Move, Attack};
    State currState = State.Move;

    float ticks = 0.0f;
    const float INTERVAL = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        setMovespeed(Random.Range(0.5f, 0.8f));
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
        moveDir.x = playerDir.x * getMovespeed(); //Change to move speed
        moveDir.y = playerDir.y * getMovespeed();
        float angle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;
        this.getRigidBody().rotation = angle;
        ticks = 0.0f;
    }

    void Attack()
    {
        moveDir.x = 0.0f;
        moveDir.y = 0.0f;

        ticks += Time.deltaTime;
        if(ticks >= INTERVAL)
        {
            Debug.Log("Attack Player");
            //+Damage player
            ticks = 0.0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Player")
        {
            //+Damage player
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
}
