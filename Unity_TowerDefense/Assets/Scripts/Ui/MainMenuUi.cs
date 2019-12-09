using UnityEngine;

public class MainMenuUi : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject quitPanel;
    [SerializeField] private GameObject newGameBtns;
    
    public void PlayButtonClick()
    {
        newGameBtns.SetActive(!newGameBtns.activeSelf);
    }
    
    public void SettingsMenuToggle()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
        menuPanel.SetActive(!menuPanel.activeSelf);
        newGameBtns.SetActive(false);
    }

    public void CreditsMenuToggle()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
        menuPanel.SetActive(!menuPanel.activeSelf);
        newGameBtns.SetActive(false);
    }

    public void QuitMenuToggle()
    {
        quitPanel.SetActive(!quitPanel.activeSelf);
        newGameBtns.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("I quit");
        Application.Quit();
    }
}
