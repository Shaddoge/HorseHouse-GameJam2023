using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField] private List<Transform> spawnLocations;
    [SerializeField] private GameObject enemyCopy;
    float ticks = 0.0f;
    [SerializeField] float spawnInterval = 2.0f;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Update()
    {
        ticks += Time.deltaTime;
        if(ticks >= spawnInterval)
        {
            Transform newPos = this.spawnLocations[Random.Range(0, spawnLocations.Count)].transform;
            GameObject newEnemy = GameObject.Instantiate(enemyCopy, newPos);
            ticks = 0.0f;
        }
    }
}
