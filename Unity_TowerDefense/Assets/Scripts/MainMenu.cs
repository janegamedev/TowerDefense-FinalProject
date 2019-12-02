using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;
    public GameObject quitPanel;
    public GameObject newGameBtns;

    public void startGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    public void newGame()
    {   
            newGameBtns.SetActive(!newGameBtns.activeSelf);
    }
    public void mainMenuToggle()
    {
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void settingsMenuToggle()
    {
        settingsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void CreditsMenuToggle()
    {
        creditsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void QuitMenuToggle()
    {
        quitPanel.SetActive(!quitPanel.activeSelf);
    }

    public void Quit()
    {
        Debug.Log("I quit");
        
        Application.Quit();
    }
}
