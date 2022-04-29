using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private enum SpawnState
    {
        Spawning,
        Waiting,
        Counting
    }
    
    [SerializeField] private Transform zombie;
    [SerializeField] private Transform wizard;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float waveCountdown;
    [SerializeField] private GameObject countDownCanvas;
    [SerializeField] private Text waveCountDownText;
    [SerializeField] private Text levelCounter;

    private int  _waveNumber = 1;
    private SpawnState _state = SpawnState.Counting;
    private float _searchCountDown = 1f;
    private double _initialWizardPercentageChance = 0.15;

    

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced");
        }
        waveCountdown = timeBetweenWaves;
    }

    private void Update () {
        if (_state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {            
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        if (waveCountdown <= 0) {
            countDownCanvas.SetActive(false);
            if (_state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave());
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
            waveCountDownText.text = Mathf.Round(waveCountdown).ToString();
        }
    }

    private void WaveCompleted()
    {
        Debug.Log("Wave completed");
        _state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;
        countDownCanvas.SetActive(true);
    }

    private bool EnemyIsAlive()
    {
        _searchCountDown -= Time.deltaTime;
        if (!(_searchCountDown <= 0f)) return true;
        _searchCountDown = 1f;
        if (GameObject.FindGameObjectsWithTag("Zombie").Length == 0 && GameObject.FindGameObjectsWithTag("Wizard").Length == 0) return false;
        return true;
    }

    private IEnumerator SpawnWave()
    {
        _state = SpawnState.Spawning;
        var howManyEnemiesToSpawn = 6 + _waveNumber * _waveNumber/2;
       
      
        for (int i = 0; i < howManyEnemiesToSpawn ; i++)
        {
            var spawningEnemy = Random.value <= 0.15 ? wizard : zombie;
            if (_waveNumber % 5 == 0)
            {
                spawningEnemy = Random.value <= _initialWizardPercentageChance + 0.05 ? wizard : zombie;
            }
            SpawnAnEnemy(spawningEnemy);
            yield return new WaitForSeconds(0.5f);
        }
        _waveNumber++;
        levelCounter.text = _waveNumber.ToString();
        _state = SpawnState.Waiting;
    }
    private void SpawnAnEnemy(Transform enemy)
    {
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, sp.position, sp.rotation);
    }
}