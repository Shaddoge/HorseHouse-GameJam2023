using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject[] spawners;
    private int levelCount = 0;


    // Start is called before the first frame update
    private void OnEnable()
    {
        EventManager.Instance.isTransitioning += IsTransitioning;
    }
    private void OnDisable()
    {
        EventManager.Instance.isTransitioning -= IsTransitioning;
    }

    // Update is called once per frame
    void Start()
    {
        foreach(var spawner in spawners)
        {
            spawner.SetActive(false);
        }

        spawners[0].SetActive(true);
    }

    public void IsTransitioning(int era)
    {
        GameObject[] allEnemies;

        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject enemy in allEnemies)
        {
            Destroy(enemy);
        }

        spawners[levelCount].SetActive(false);

        levelCount++;
        StartCoroutine(ResetTransition());
        
    }

    IEnumerator ResetTransition()
    {
        yield return new WaitForSeconds(5.0f);
        spawners[levelCount].SetActive(true);
    }
}
