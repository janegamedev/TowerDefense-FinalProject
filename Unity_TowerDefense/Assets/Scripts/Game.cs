using UnityEditor.MemoryProfiler;
using UnityEngine;

public class Game : Singleton<Game>
{
    public Events.GameSaveCompleted OnGameUpdateCompleted;
    
    public int stars = 3;
    public int currentLevelUnlocked = 0;
    
    private int levelsAmount = 11;
    
    public LevelState[] levelStates;
    private int[] levelStatesInts;
    [HideInInspector] public int[] levelScore;

    private int _maxLevel = 10;
    private string path;

    public string Path
    {
        get
        {
            return path;
        }
        set
        {
            path = value;
            Debug.Log("Value changed" + path);
        }
    }

    //Settings
    private float _musicVolume;
    private float _effectsVolume;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        levelStates = new LevelState[levelsAmount-1];
        levelStatesInts = new int[levelsAmount-1];
        for (int i = 0; i < levelStates.Length; i++)
        {
            levelStates[i] = LevelState.LOCKED;
            levelStatesInts[i] = 0;
        }

        levelStates[0] = LevelState.UNLOCKED;
        levelStatesInts[0] = 1;
        
        currentLevelUnlocked ++;
        
        levelScore = new int[levelsAmount-1];
        for (int i = 0; i < levelScore.Length; i++)
        {
            levelScore[i] = 0;
        }
    }

    public void FinishLevel(int levelIndex, int score)
    {
        if (score > 0)
        {
            if (levelStates[levelIndex - 1] == LevelState.UNLOCKED)
            {
                levelScore[levelIndex - 1] = score;
                levelStates[levelIndex - 1] = LevelState.FINISHED;
                levelStates[levelIndex] = LevelState.UNLOCKED;
                stars += score;

                if (currentLevelUnlocked < _maxLevel)
                {
                    currentLevelUnlocked++;
                }
            }
            else
            {
                if (score > levelScore[levelIndex - 1])
                {
                    int temp = score - levelScore[levelIndex - 1];
                    levelScore[levelIndex - 1] = score;
                }
            }
        }
        
        SaveGame();
    }

    public void SaveGame()
    {
        Debug.Log("Save game " + path);
        SaveSystem.SaveGame(this);
        OnGameUpdateCompleted.Invoke();
    }

    public void LoadGame()
    {
        GameData data = SaveSystem.LoadGame(path);

        if (data != null)
        {
            Debug.Log("Game was loaded");
            stars = data.stars;
            currentLevelUnlocked = data.level;
            levelStates = data.levelStates;
            levelScore = data.levelScore;
            
            
            OnGameUpdateCompleted.Invoke();
        }
        else
        {
            Debug.Log("Saving game form Load game method " + path);
            SaveGame();
        }
    }

    public void SetPath(string p)
    {
        Path = p;
        LoadGame();
    }
}