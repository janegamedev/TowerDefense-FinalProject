using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuUi : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;
    public GameObject quitPanel;
    public GameObject newGameBtns;
    
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
