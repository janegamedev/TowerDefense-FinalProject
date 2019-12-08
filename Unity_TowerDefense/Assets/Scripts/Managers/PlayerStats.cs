using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class PlayerStats : Singleton<PlayerStats>
{
    private int coins = 550;
    public int Coins => coins;

    private int lives = 5;
    public int Lives => lives;

    private int currentWave;
    public int CurrentWave => currentWave;

    private int wavesTotal;

    private float _sellPercentage = 0.7f;
    public float SellPercentage => _sellPercentage;
    
    public int WavesTotal
    {
        get => wavesTotal;
        set => wavesTotal = value;
    }

    private InGameUi _inGameUi;

    private void Start()
    {
        _inGameUi = FindObjectOfType<InGameUi>();
        
        // all data set up
        
        _inGameUi.UpdateUi();
    }

    public void ChangeCoinsAmount(int amount)
    {
        coins -= amount;
        _inGameUi.UpdateUi();
    }

    public void ChangeLives(int amount)
    {
        lives -= amount;
        _inGameUi.UpdateUi();
    }

    public void ChangeCurrentWave(int number)
    {
        currentWave = number;
        _inGameUi.UpdateUi();
    }

    public void GetBounty(int amount)
    {
        coins += amount;
        _inGameUi.UpdateUi();
    }

    public int EndGame()
    {
        if (lives >= 16 && lives <= 20)
        {
            return 3;
        }
        else if(lives >= 8 && lives <= 15)
        {
            return 2;
        }
        else if(lives >= 1 && lives <= 7)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}