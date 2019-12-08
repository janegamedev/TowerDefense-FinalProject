using System;
using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    public Level[] levels;

    private Game _gameStats;

    private void Start()
    {
        _gameStats = FindObjectOfType<Game>();
        _gameStats.OnGameUpdateCompleted.AddListener(Init);
    }

    private void Init()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].score = _gameStats.levelScore[i];
            levels[i].UpdateState(_gameStats.levelStates[i]);
        }
    }

    private void OnDestroy()
    {
        _gameStats.OnGameUpdateCompleted.RemoveListener(Init);
    }
}