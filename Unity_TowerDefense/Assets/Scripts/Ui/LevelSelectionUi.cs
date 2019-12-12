using TMPro;
using UnityEngine;

public class LevelSelectionUi : MonoBehaviour
{
    [SerializeField] private GameObject selectPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject encyclopediaPanel;

    [SerializeField] private TextMeshProUGUI starsTmp;

    private LevelSO _currentLevelSelected;
    private AudioSource _audioSource;

    [SerializeField] private AudioClip buttonClickSfx;
    [SerializeField] private AudioClip upgradeSfx;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void QuitToMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }

    public void ToggleSettings()
    {
        PlayClickSfx();
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void ToggleUpgrades()
    {
        PlayClickSfx();
        upgradePanel.SetActive(!upgradePanel.activeSelf);
    }
    public void ToggleEncyclopedia()
    {
        PlayClickSfx();
        encyclopediaPanel.SetActive(!encyclopediaPanel.activeSelf);
    }

    public void ToggleLevel(LevelSO levelSo)
    {
        PlayClickSfx();
        _currentLevelSelected = levelSo;
        levelPanel.SetActive(true);
    }

    public void CloseLevel()
    {
        PlayClickSfx();
        _currentLevelSelected = null;
        levelPanel.SetActive(false);
    }

    public void StartLevel()
    {
        GameManager.Instance.LoadLevel(_currentLevelSelected);
    }

    public void UpdateStars()
    {
        starsTmp.text = Game.Instance.stars.ToString();
    }
    
    public void PlayClickSfx()
    {
        _audioSource.Stop();
        _audioSource.clip = buttonClickSfx;
        _audioSource.Play();
    }

    public void PlayUpgrade()
    {
        _audioSource.Stop();
        _audioSource.clip = upgradeSfx;
        _audioSource.Play();
    }
}
