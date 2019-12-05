using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsTmp;
    [SerializeField] private TextMeshProUGUI livesTmp;
    [SerializeField] private TextMeshProUGUI wavesTotalTmp;
    [SerializeField] private TextMeshProUGUI currentWaveTmp;

    public void UpdateUi()
    {
        coinsTmp.text = PlayerStats.Instance.Coins.ToString();
        livesTmp.text = PlayerStats.Instance.Lives.ToString();
        wavesTotalTmp.text = PlayerStats.Instance.WavesTotal.ToString();
        currentWaveTmp.text = PlayerStats.Instance.CurrentWave.ToString();
    }
}