using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField]
    private List<Sprite> sprites = new();
    private Vector3 target;
    NavMeshAgent agent;
    private SpriteRenderer sprite;

    [SerializeField] Player player;
    [SerializeField] public GameObject hpBar;
    [SerializeField] private AudioClip dieSFX;
    enum State { Move, Attack };
    State currState = State.Move;

    float ticks = 0.0f;
    const float INTERVAL = 2.0f;

    Action<Enemy> killAction;

    private Color32 origColor;

    public int damage = 1;
    
    private void Awake()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        /*agent.updateRotation = false;
        agent.updateUpAxis = false;*/
    }

    private void OnEnable()
    {
        switch (GameManager.Instance.CurrentEra)
        {
            case Era.Space:
                sprite.sprite = sprites[0];
                break;
            case Era.Modern:
                sprite.sprite = sprites[1];
                break;
            case Era.Stone:
                sprite.sprite = sprites[2];
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //setMovespeed(UnityEngine.Random.Range(0.5f, 0.8f));
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case State.Move: Move(); break;
            case State.Attack: Attack(); break;
        }
        hpBar.GetComponent<EnemyHPBar>().SetHealth(this.health, this.maxHealth);
    }

    void Move()
    {
        target = player.transform.position;

        agent.SetDestination(target);
    }

    void Attack()
    {
        moveDir.x = 0.0f;
        moveDir.y = 0.0f;

        ticks += Time.deltaTime;
        if (ticks >= INTERVAL)
        {
            ticks = 0.0f;
            EventManager.Instance.TakeDamage(this.damage);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
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

    public override void TakeDamage(int damage)
    {
        if (health - damage > 0)
            StartCoroutine(DamageFeedback());
        base.TakeDamage(damage);
    }

    public override void Die()
    {
        if (dieSFX != null)
            AudioManager.Instance.PlaySFX(dieSFX, UnityEngine.Random.Range(0.3f, 0.8f));
        killAction(this);
        EventManager.Instance.EnemyDeath(1, transform.position);
        health = maxHealth;
        //this.GetComponent<SpriteRenderer>().color = new Color(245 / 255.0f, 119 / 255.0f, 110 / 255.0f);
    }

    public void Init(Action<Enemy> _killAction)
    {
        killAction = _killAction;
    }

    public override void FixedUpdate()
    {
        return;
    }

    private IEnumerator DamageFeedback()
    {
        //sprite.color = new Color32(230, 60, 60, 255);
        yield return new WaitForSeconds(0.05f);
        //sprite.color = origColor;
    }
}
