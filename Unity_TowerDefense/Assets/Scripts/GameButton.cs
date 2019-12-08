using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameButton : MonoBehaviour
{
    [SerializeField] private Button newGame_btn;
    [SerializeField] private Button loadGame_btn;
    [SerializeField] private Button deleteSave_btn;
    [SerializeField] private int buttonId;

    [SerializeField] private bool loadGame;

    public string _path;
    private void Start()
    {
        _path = Application.persistentDataPath + "/slot" + buttonId + ".txt";
        CheckForSaves();
    }

    private void CheckForSaves()
    {
        if (SaveSystem.LoadGame(_path) == null)
        {
            loadGame = false;
            newGame_btn.gameObject.SetActive(true);
            newGame_btn.onClick.AddListener(OnClick);
            loadGame_btn.gameObject.SetActive(false);
        }
        else
        {
            loadGame = true;
            newGame_btn.gameObject.SetActive(false);
            loadGame_btn.gameObject.SetActive(true);
            loadGame_btn.onClick.AddListener(OnClick);
            deleteSave_btn.onClick.AddListener(DeleteSave);
        }
    }

    public void OnClick()
    {
        GameManager.Instance.LoadLevelSelection(_path);
    }

    public void DeleteSave()
    {
        SaveSystem.DeleteSave(_path);
        CheckForSaves();
    }
}
