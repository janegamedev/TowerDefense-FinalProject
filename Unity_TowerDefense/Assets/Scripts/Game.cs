using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
    public int stars = 3;
    public int level = 0;

    public string path;
    
    public void UnlockNextLevel()
    {
        
    }

    public void GetStars()
    {

    }

    private void SaveGame()
    {
        SaveSystem.SaveGame(this);
    }

    private void LoadGame()
    {
        GameData data = SaveSystem.LoadGame(path);

        if (data != null)
        {
            Debug.Log("Game was loaded");
            stars = data.stars;
            level = data.level;
        }
    }

    public void SetPath(string p)
    {
        Debug.Log(Time.deltaTime + " path");
        path = p;
        LoadGame();
        SaveGame();
    }

    private void OnDestroy()
    {
        SaveGame();
    }
}