using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Wave
{
    public EnemySet[] enemySets;
    public float timeBetweenSpawns;
    public float timeBetweenSets;
    
}

[Serializable]
public class EnemySet
{
    public int enemyCount;
    public EnemySO enemyData;
}

public class WavesManager : MonoBehaviour
{
    public Wave[] waves;

    public Waypoint[] spawnWaypoints;

    public Wave currentWave;
    public EnemySet currentEnemySet;
    public int currentWaveNumber;
    public int currentSetNumber;

    public int enemiesRemainingToSpawn;
    public int enemiesRemainingAlive;
    public float nextSpawnTime;
    
    public int totalEnemyCount;

    private void Start()
    {
        NextWave();

        foreach (var wave in waves)
        {
            foreach (var set in wave.enemySets)
            {
                totalEnemyCount += set.enemyCount;
            }
        }

        PlayerStats.Instance.WavesTotal = waves.Length;
    }

    private void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
            
            Waypoint randomWaypoint = spawnWaypoints[Random.Range(0, spawnWaypoints.Length)];

            EnemySO enemyData = currentEnemySet.enemyData;
            Enemy spawnedEnemy = Instantiate(enemyData.enemyModel, randomWaypoint.transform.position,
                Quaternion.identity).GetComponent<Enemy>();
            
            spawnedEnemy.Init(enemyData, randomWaypoint);

            spawnedEnemy.OnDeath += OnEnemyDeath;
        }
        else
        {
            
        }
    }

    void OnEnemyDeath()
    {
        enemiesRemainingAlive--;

        if (enemiesRemainingAlive == 0)
        {
            NextSet();
        }
    }


    private void NextSet()
    {
        currentSetNumber++;
        
        if (currentSetNumber - 1 < currentWave.enemySets.Length)
        {
            currentEnemySet = currentWave.enemySets[currentSetNumber-1];
            
            enemiesRemainingToSpawn = currentEnemySet.enemyCount;
            enemiesRemainingAlive = currentEnemySet.enemyCount;
        }
        else
        {
            NextWave();
        }
    }

    private void NextWave()
    {
        currentWaveNumber++;
        if (currentWaveNumber - 1 < waves.Length)
        {
            PlayerStats.Instance.ChangeCurrentWave(currentWaveNumber);
            currentWave = waves[currentWaveNumber - 1];
            currentSetNumber = 0;
            
            NextSet();
        }
        else
        {
            Debug.Log(currentWaveNumber + "Level finished");
        }
    }
}
