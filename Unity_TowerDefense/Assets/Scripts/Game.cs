using UnityEngine;

public class Game : MonoBehaviour
{
    public Events.GameSaveCompleted OnGameSaveCompleted;
    
    public int stars = 3;
    public int level = 0;
    
    private int levelsAmount = 11;
    
    [HideInInspector] public LevelState[] levelStates;
    [HideInInspector] public int[] levelScore;
    
    [HideInInspector] public string path;
    private void Start()
    {
        levelStates = new LevelState[levelsAmount-1];
        for (int i = 0; i < levelStates.Length; i++)
        {
            Debug.Log(i);
            levelStates[i] = LevelState.LOCKED;
        }

        levelStates[0] = LevelState.UNLOCKED;
        
        levelScore = new int[levelsAmount-1];
        for (int i = 0; i < levelScore.Length; i++)
        {
            levelScore[i] = 0;
        }
    }

    public void UnlockNextLevel()
    {
        level++;
    }

    public void GetStars(int amount)
    {
        stars += amount;
    }

    private void SaveGame()
    {
        SaveSystem.SaveGame(this);
        OnGameSaveCompleted.Invoke();
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