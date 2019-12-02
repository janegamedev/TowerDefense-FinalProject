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
        menuPanel.SetActive(!menuPanel.activeSelf);
        settingsPanel.SetActive(!settingsPanel.activeSelf);
        creditsPanel.SetActive(!creditsPanel.activeSelf);
        newGameBtns.SetActive(!newGameBtns.activeSelf);
    }

    public void settingsMenuToggle()
    {

        settingsPanel.SetActive(!settingsPanel.activeSelf);
        menuPanel.SetActive(!menuPanel.activeSelf);
        newGameBtns.SetActive(!newGameBtns.activeSelf);
    }

    public void CreditsMenuToggle()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
        menuPanel.SetActive(!menuPanel.activeSelf);
        newGameBtns.SetActive(!newGameBtns.activeSelf);
    }

    public void QuitMenuToggle()
    {
        quitPanel.SetActive(!quitPanel.activeSelf);
        newGameBtns.SetActive(!newGameBtns.activeSelf);
    }

    public void Quit()
    {
        Debug.Log("I quit");
        
        Application.Quit();
    }
}
