using System;
using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    [SerializeField] private Level[] levels;
    [SerializeField] private TweenAnimation pin;

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

        pin.transform.position = levels[_gameStats.currentLevelUnlocked - 1].transform.position + Vector3.up;
        pin.enabled = true;
    }

    private void OnDestroy()
    {
        _gameStats.OnGameUpdateCompleted.RemoveListener(Init);
    }
}