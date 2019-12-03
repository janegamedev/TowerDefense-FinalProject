using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : Singleton<PlayerStats>
{
    public int currency;
    public TextMeshProUGUI currencyUi;

    public int lives;
    public TextMeshProUGUI livesUi;

    public int wavesTotal;
    public int currentWave;

    public TextMeshProUGUI wavesTotalUi;
    public TextMeshProUGUI currentWaveUi;

    private void Update()
    {
        currencyUi.text = currency.ToString();
        livesUi.text = lives.ToString();
        wavesTotalUi.text = wavesTotal.ToString();
        currentWaveUi.text = currentWave.ToString();
    }
}
