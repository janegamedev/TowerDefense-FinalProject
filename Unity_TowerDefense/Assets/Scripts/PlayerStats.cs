using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : Singleton<PlayerStats>
{
    private int coins = 150;
    public int Coins => coins;

    private int lives;
    public int Lives => lives;

    private int currentWave;
    public int CurrentWave => currentWave;

    private int wavesTotal;

    public int WavesTotal
    {
        get => wavesTotal;
        set => wavesTotal = value;
    }

    public UiManager _uiManager;

    private void Start()
    {
        _uiManager = GetComponent<UiManager>();
        
        // all data set up
        
        _uiManager.UpdateUi();
    }

    public void ChangeCoinsAmount(int amount)
    {
        coins -= amount;
        _uiManager.UpdateUi();
    }

    public void ChangeLives(int amount)
    {
        lives -= amount;
        _uiManager.UpdateUi();
    }

    public void ChangeCurrentWave(int number)
    {
        currentWave = number;
        _uiManager.UpdateUi();
    }

    public void GetBounty(int amount)
    {
        coins += amount;
        _uiManager.UpdateUi();
    }
}