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

        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage(1);
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
        moveDir.x = 0.0f;
        moveDir.y = 0.0f;

        ticks += Time.deltaTime;
        if(ticks >= INTERVAL)
        {
            Debug.Log("Attack Player");
            ticks = 0.0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

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

    //public void TakeDamage(int damage) 
    //{
    //    this.GetComponent<SpriteRenderer>().color = new Color(171/255.0f, 45/255.0f, 36/255.0f);
    //    StartCoroutine(ResetSpriteColor());
    //}

    //IEnumerator ResetSpriteColor()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    this.GetComponent<SpriteRenderer>().color = new Color(245 / 255.0f, 119 / 255.0f, 110 / 255.0f);
    //}

    //void Die()
    //{
    //    Destroy(this); 
    //}
}
