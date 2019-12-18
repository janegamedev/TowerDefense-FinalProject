using System;
using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    [SerializeField] private Level[] levels;
    [SerializeField] private TweenAnimation pin;

    private Game _game;

    private void Start()
    {
        _game = FindObjectOfType<Game>();
        _game.OnGameUpdateCompleted.AddListener(Init);
    }

    private void Init()
    {
        pin.enabled = false;
        
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].score = _game.levelScore[i];
            levels[i].UpdateState(_game.levelStates[i]);
        }

        pin.transform.position = levels[_game.currentLevelUnlocked - 1].transform.position + Vector3.up;
        pin.enabled = true;
    }

    private void OnDestroy()
    {
        _game.OnGameUpdateCompleted.RemoveListener(Init);
    }
}