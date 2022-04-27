using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private enum SpawnState
    {
        Spawning,
        Waiting,
        Counting
    };
    
    [System.Serializable]
    public class Wave
    {
        public string name;
        public int count;
        public float rate;
    }
    
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Wave[] waves;
    private int nextWave = 0;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float waveCountdown;
    
    private SpawnState state = SpawnState.Counting;
    private float searchCountDown = 1f;
    
    private int _randomSpawnPoint;
    private int _randomEnemy;
    public static bool spawnAllowed;

    private void Start()
    {
        spawnAllowed = true;
        InvokeRepeating("SpawnAnEnemy", 0f, 1f);
    }

    private void SpawnAnEnemy()
    {
        if (spawnAllowed)
        {
            _randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            _randomEnemy = Random.Range(0, enemies.Length);
            Instantiate(enemies[_randomEnemy], spawnPoints[_randomSpawnPoint].position, Quaternion.identity);
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave completed");

        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All waves Complete.. LOOOPING");
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerable SpawnWave(Wave _wave)
    {
        Debug.Log("SPawning Wave: ");
        state = SpawnState.Spawning;
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnAnEnemy();
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.Waiting;
        yield break;
    }
}