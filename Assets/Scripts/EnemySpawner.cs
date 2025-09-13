using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    float SpawnFrequency;
    private float currentSpawnTimer;

    [SerializeField]
    float spawnRadius;

    [SerializeField]
    GameObject enemyObj;


    private void Update()
    {
        currentSpawnTimer -= Time.deltaTime;
        if (currentSpawnTimer <= 0)
        {
            currentSpawnTimer = SpawnFrequency;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyObj, PlayerCharacterMovement.Instance.transform.position + (new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f) * spawnRadius)), Quaternion.identity);
    }
}
