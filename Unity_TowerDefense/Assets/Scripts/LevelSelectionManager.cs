using System;
using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    public Level[] levels;

    private Game _gameStats;

    private void Start()
    {
        _gameStats = FindObjectOfType<Game>();
        _gameStats.OnGameSaveCompleted.AddListener(Init);
    }

    private void Init()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            Debug.Log(i);
            levels[i].UpdateState(_gameStats.levelStates[i]);
            levels[i].score = _gameStats.levelScore[i];
        }
    }

    private void OnDestroy()
    {
        _gameStats.OnGameSaveCompleted.RemoveListener(Init);
    }
}