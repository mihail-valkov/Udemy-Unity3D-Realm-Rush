using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int poolSize = 10;
    public int enemiesToSpawn = 10;
    private List<GameObject> enemies;
    float instantiationInterval = 1f;


    // Start is called before the first frame update
    void Start()
    {
        //Begin instantiating enemies 1 per second using coroutine
        StartCoroutine(InstantiateEnemiesOverTime());
    }

    private void Awake()
    {
        PopulatePool();
    }

    private void PopulatePool()
    {
        enemies = new List<GameObject>(poolSize);
        //Instantiate the pool of enemies
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform);
            enemy.SetActive(false);
            enemy.GetComponent<EnemyHealth>().Pool = this;
            enemies.Add(enemy);
        }
    }

    IEnumerator InstantiateEnemiesOverTime()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            InstantiateEnemy();
            yield return new WaitForSeconds(instantiationInterval);
        }
    }

    void InstantiateEnemy()
    {
        GameObject enemy;
        //check if there are any inactive enemies in the pool
        if (enemies.Count > 0)
        {
            enemy = enemies[enemies.Count - 1];
            enemies.RemoveAt(enemies.Count - 1);
            enemy.SetActive(true);
            enemy.GetComponent<EnemyMover>().RestartEnemy();
        }
        else
        {
            enemy = Instantiate(enemyPrefab, transform);
            enemy.GetComponent<EnemyHealth>().Pool = this;
        }
    }

    //upon destruction of the object pool, destroy all enemies in the pool
    private void OnDestroy()
    {
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
