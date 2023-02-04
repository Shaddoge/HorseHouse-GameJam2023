using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy prefab;
    [SerializeField] private int spawnAmount = 20;
    [SerializeField] private bool usePool;

    private ObjectPool<Enemy> pool;

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
        }, false, 10, 20);

        InvokeRepeating(nameof(Spawn), 0.2f, 0.2f);
    }

    private void Spawn()
    {
        for(var i = 0; i < spawnAmount; i++)
        {
            var enemy = usePool ? pool.Get() : Instantiate(prefab);

            enemy.Init(KillEnemy);
        }
    }

    private void KillEnemy(Enemy enemy)
    {
        if (usePool)
        {
            pool.Release(enemy);
        }
        else
        {
            Destroy(enemy.gameObject);
        }
        
    }
}
