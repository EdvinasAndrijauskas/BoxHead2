using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemies;
    private int randomSpawnPoint, randomEnemy;
    public static bool spawnAllowed;
    private void Start()
    {
        spawnAllowed = true;
        InvokeRepeating("SpawnAnEnemy",0f,1f);
    }
   private void SpawnAnEnemy()
    {
        if (spawnAllowed)
        {
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            randomEnemy = Random.Range(0, enemies.Length);
            Instantiate(enemies[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
        }
    }
}
