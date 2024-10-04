using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float spawnTimer = 20f;
    public GameObject enemy;
    public GameObject strongerEnemy;
    System.Random random;
    float strongSpawnChance = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();
        InvokeRepeating("SpawnEnemies", 0f, spawnTimer);
    }

    void SpawnEnemies()
    {
        if (!Physics.CheckSphere(transform.position, .1f))
        {
            if ((int)(random.NextDouble() * 1/strongSpawnChance) == 0)
            {
                Instantiate(strongerEnemy, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(enemy, transform.position, Quaternion.identity);
            }
        }
    }
}
