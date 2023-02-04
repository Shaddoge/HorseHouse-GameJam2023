using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy prefab;
    [SerializeField] private int spawnAmount = 2;
    [SerializeField] private bool usePool;

    private ObjectPool<Enemy> pool;
    [SerializeField] private float spawnDelay = 0.5f;
    [SerializeField] private float timePassed = 0;
    [SerializeField] private int maxSpawnCount = 8;
    [SerializeField] private int spawnCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        pool = new ObjectPool<Enemy>(() =>
        {
            return Instantiate(prefab);
        }, enemy =>
        {
            enemy.gameObject.SetActive(true);
        }, enemy =>
        {
            enemy.gameObject.SetActive(false);
        }, enemy =>
        {
            Destroy(enemy.gameObject);
        }, true, 8, 10);

        //InvokeRepeating(nameof(Spawn), 0.2f, 0.2f);

    }

    private void Spawn()
    {
        for(var i = 0; i < spawnAmount; i++)
        {
            var enemy = pool.Get();

            Vector2 newPosition;
            newPosition.x = this.transform.position.x;
            newPosition.y = this.transform.position.y;

            newPosition += Random.insideUnitCircle * 2;

            enemy.transform.position = newPosition;

            enemy.Init(KillEnemy);
        }
    }

    private void KillEnemy(Enemy enemy)
    {
        if (usePool)
        {
            pool.Release(enemy);
            spawnCount--;
        }
    }

    private void Update()
    {
        timePassed+= Time.deltaTime;

        if (timePassed >= spawnDelay)
        {
            timePassed = 0;
            if (spawnCount >= maxSpawnCount)
                return;

            spawnCount += spawnAmount;
            Spawn();
        }

    }
}
