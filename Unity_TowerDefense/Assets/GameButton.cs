using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameButton : MonoBehaviour
{
    [SerializeField] private GameObject newGameImage;
    [SerializeField] private GameObject loadGameImage;
    [SerializeField] private int buttonId;

    [SerializeField] private bool loadGame;

    public string _path;
    private void Start()
    {
        _path = Application.persistentDataPath + "/slot" + buttonId + ".txt";
        
        if (SaveSystem.LoadGame(_path) == null)
        {
            loadGame = false;
            newGameImage.SetActive(true);
            loadGameImage.SetActive(false);
        }
        else
        {
            loadGame = true;
            newGameImage.SetActive(false);
            loadGameImage.SetActive(true);
        }
    }

    public void OnClick()
    {
        GameManager.Instance.LoadLevelSelection(_path);
    }
}
