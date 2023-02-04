using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] Player player;
    [SerializeField] Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rigidBody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerDir = player.transform.position - this.transform.position;
        moveDir.x = playerDir.x * 0.2f; //Change to move speed
        moveDir.y = playerDir.y * 0.2f;
        float angle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;
        this.rigidBody.rotation = angle;
    }
}
