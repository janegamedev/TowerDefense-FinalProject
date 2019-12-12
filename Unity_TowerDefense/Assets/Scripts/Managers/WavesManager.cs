using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
public class Wave
{
    public EnemySet[] enemySets;
    public float timeBetweenSpawns;
}

[Serializable]
public class EnemySet
{
    public int enemyCount;
    public EnemySO enemyData;
}

public class WavesManager : MonoBehaviour
{
    [SerializeField] private Button waveButton;
    private Wave[] waves;
    private RoadTile[] spawnTiles;

    public Wave currentWave;
    public EnemySet currentEnemySet;
    public int currentWaveNumber;
    public int currentSetNumber;

    public int enemiesRemainingToSpawn;
    public int enemiesRemainingAlive;
    public float nextSpawnTime;
    
    public int totalEnemyCount;
    
    private bool _canSpawn;
    private bool _isFinished;
    private bool _isInit;
    
    public void Init(LevelSO levelData)
    {
        waves = levelData.waves;
        _isInit = true;
        spawnTiles = FindObjectsOfType<RoadTile>().Where(x => x.isStart).ToArray();
        
        PlayerStats.Instance.WavesTotal = waves.Length;
        waveButton.onClick.AddListener(StartWave);
    }
    
    private void Update()
    {
        if (_isInit)
        {
            if (_canSpawn)
            {
                _canSpawn = false;
                if (enemiesRemainingToSpawn > 0)
                {
                    StartCoroutine(EnemySpawn());
                }
                else
                {
                    NextSet();
                }
            }

            if (enemiesRemainingAlive <= 0)
            {
                waveButton.interactable = true;

                if (currentWaveNumber >= waves.Length)
                {
                    EndLevel();
                }
            }
        }
    }

    void OnEnemyDeath(Enemy enemy)
    {
        enemiesRemainingAlive--;
        
        if (enemy.Health > 0)
        {
            AudioManager.Instance.PlayerSfx(AudioManager.Instance.liveDecreaseSfx);
            PlayerStats.Instance.ChangeLives(1);

            if (PlayerStats.Instance.Lives <= 0)
            {
                EndLevel();
            }
        }
        else
        {
            AudioManager.Instance.PlayerSfx(AudioManager.Instance.bountySfx);
            PlayerStats.Instance.GetBounty(enemy.Bounty);
        }
        
        Destroy(enemy.gameObject);

        if (enemiesRemainingAlive <= 0)
        {
            NextSet();
        }
    }

    IEnumerator EnemySpawn()
    {
        yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        enemiesRemainingToSpawn--;
            
        RoadTile randomWaypoint = spawnTiles[Random.Range(0, spawnTiles.Length)];

        EnemySO enemyData = currentEnemySet.enemyData;
        Enemy spawnedEnemy = Instantiate(enemyData.enemyModel, randomWaypoint.transform.position,
            Quaternion.identity).GetComponent<Enemy>();
        spawnedEnemy.Init(enemyData, randomWaypoint);
        spawnedEnemy.OnDeath += OnEnemyDeath;

        _canSpawn = true;
    }

    private void NextSet()
    {
        currentSetNumber++;
        
        if (currentSetNumber - 1 < currentWave.enemySets.Length)
        {
            currentEnemySet = currentWave.enemySets[currentSetNumber-1];
            
            enemiesRemainingToSpawn = currentEnemySet.enemyCount;
            enemiesRemainingAlive = currentEnemySet.enemyCount;
            _canSpawn = true;
        }
    }

    private void StartWave()
    {
        AudioManager.Instance.PlayerSfx(AudioManager.Instance.waveStartSfx);
        currentWaveNumber++;
        waveButton.interactable = false;

        if (currentWaveNumber <= waves.Length)
        {
            PlayerStats.Instance.ChangeCurrentWave(currentWaveNumber);
            currentWave = waves[currentWaveNumber - 1];
            currentSetNumber = 0;
            NextSet();
        }
    }

    private void EndLevel()
    {
        if (!_isFinished)
        {
            _isFinished = true;
            GameManager.Instance.GameOver();
        }
    }
}
