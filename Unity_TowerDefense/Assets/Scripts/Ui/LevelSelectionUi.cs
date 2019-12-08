using UnityEngine;

public class LevelSelectionUi : MonoBehaviour
{
    [SerializeField] private GameObject selectPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject encyclopediaPanel;

    private LevelSO _currentLevelSelected;


    public void QuitToMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }

    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void ToggleUpgrades()
    {
        upgradePanel.SetActive(!settingsPanel.activeSelf);
    }
    public void ToggleEncyclopedia()
    {
        encyclopediaPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void ToggleLevel(LevelSO levelSo)
    {
        _currentLevelSelected = levelSo;
        levelPanel.SetActive(true);
    }

    public void CloseLevel()
    {
        _currentLevelSelected = null;
        levelPanel.SetActive(false);
    }

    public void StartLevel()
    {
        GameManager.Instance.LoadLevel(_currentLevelSelected);
    }
}
