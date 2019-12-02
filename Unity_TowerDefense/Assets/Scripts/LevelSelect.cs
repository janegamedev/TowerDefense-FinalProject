using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSelect : MonoBehaviour
{
    public GameObject selectPanel;
    public GameObject settingsPanel;
    //public GameObject creditsPanel;
    //public GameObject quitPanel;
    //public GameObject newGameBtns;

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //public void newGame()
    //{   
    //        newGameBtns.SetActive(!newGameBtns.activeSelf);
    //}
    //public void mainMenuToggle()
    //{
    //    selectPanel.SetActive(!selectPanel.activeSelf);
    //    settingsPanel.SetActive(!settingsPanel.activeSelf);
    //    creditsPanel.SetActive(!creditsPanel.activeSelf);
    //    newGameBtns.SetActive(!newGameBtns.activeSelf);
    //}

    public void settingsMenuToggle()
    {

        settingsPanel.SetActive(!settingsPanel.activeSelf);
        selectPanel.SetActive(!selectPanel.activeSelf);
        //newGameBtns.SetActive(!newGameBtns.activeSelf);
    }

    //public void CreditsMenuToggle()
    //{
    //    creditsPanel.SetActive(!creditsPanel.activeSelf);
    //    selectPanel.SetActive(!selectPanel.activeSelf);
    //    newGameBtns.SetActive(!newGameBtns.activeSelf);
    //}

    //public void QuitMenuToggle()
    //{
    //    quitPanel.SetActive(!quitPanel.activeSelf);
    //    newGameBtns.SetActive(!newGameBtns.activeSelf);
    //}

    public void Quit()
    {
        Debug.Log("I quit");
        
        Application.Quit();
    }
}
