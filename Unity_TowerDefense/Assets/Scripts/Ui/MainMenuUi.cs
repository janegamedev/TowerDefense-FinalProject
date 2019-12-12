using System;
using UnityEngine;

public class MainMenuUi : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject quitPanel;
    [SerializeField] private GameObject newGameBtns;

    [SerializeField] private AudioClip buttonClickSfx;
    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayButtonClick()
    {
        PlayClickSfx();
        newGameBtns.SetActive(!newGameBtns.activeSelf);
    }
    
    public void SettingsMenuToggle()
    {
        PlayClickSfx();
        settingsPanel.SetActive(!settingsPanel.activeSelf);
        menuPanel.SetActive(!menuPanel.activeSelf);
        newGameBtns.SetActive(false);
    }

    public void CreditsMenuToggle()
    {
        PlayClickSfx();
        creditsPanel.SetActive(!creditsPanel.activeSelf);
        menuPanel.SetActive(!menuPanel.activeSelf);
        newGameBtns.SetActive(false);
    }

    public void QuitMenuToggle()
    {
        PlayClickSfx();
        quitPanel.SetActive(!quitPanel.activeSelf);
        newGameBtns.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("I quit");
        Application.Quit();
    }

    private void PlayClickSfx()
    {
        _audioSource.Stop();
        _audioSource.clip = buttonClickSfx;
        _audioSource.Play();
    }
}
