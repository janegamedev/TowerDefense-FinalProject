using System;
using TMPro;
using UnityEngine;

public class InGameUi : MonoBehaviour
{
    //HUD
    [SerializeField] private TextMeshProUGUI coinsTmp;
    [SerializeField] private TextMeshProUGUI livesTmp;
    [SerializeField] private TextMeshProUGUI wavesTotalTmp;
    [SerializeField] private TextMeshProUGUI currentWaveTmp;
    
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject gameOverPanel;

    public void UpdateUi()
    {
        coinsTmp.text = PlayerStats.Instance.Coins.ToString();
        livesTmp.text = PlayerStats.Instance.Lives.ToString();
        wavesTotalTmp.text = PlayerStats.Instance.WavesTotal.ToString();
        currentWaveTmp.text = PlayerStats.Instance.CurrentWave.ToString();
    }
    
    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameState previousState, GameState currentState)
    {
        settingsPanel.gameObject.SetActive(currentState == GameState.PAUSED);
        gameOverPanel.gameObject.SetActive(currentState == GameState.END);
    }

    public void OnSettingsClick()
    {
        GameManager.Instance.TogglePause();
    }
    
    public void RestartGame()
    {
        GameManager.Instance.LoadLevel(GameManager.Instance.currentLevelSo);
    }

    public void QuitToSelectionMenu()
    {
        GameManager.Instance.LoadLevelSelection(Game.Instance.Path);
    }
}