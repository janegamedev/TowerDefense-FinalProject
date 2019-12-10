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
    
    public UpgradeState[] archerUpgradeStates = new UpgradeState[3];
    public UpgradeState[] mageUpgradeStates = new UpgradeState[3];
    public UpgradeState[] slowdownUpgradeStates = new UpgradeState[3];
    public UpgradeState[] bombUpgradeStates = new UpgradeState[3];
    public UpgradeState[] meteorUpgradeStates = new UpgradeState[3];
    
    //archer modifiers
    public float _archerRangeIncrease;
    public float _archerDamageIncrease;
    
    //mage modifiers
    public float _mageRangeIncrease;
    public float _mageDamageIncrease;
    
    //slowdown modifiers
    public float _slowdownRangeIncrease;
    public float _slowdownIncrease;
    
    //bomb modifiers
    public float _bombDamageIncrease;
    public float _bombRangeIncrease;
    public float _bombCostDecrease;
    
    //meteor modifiers
    public float _meteorDamageIncrease;
    public float _meteorRangeIncrease;
    public float _meteorCountDownDecrease;
    
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
        
        //Init upgrades
        for (int i = 0; i < archerUpgradeStates.Length; i++)
        {
            archerUpgradeStates[i] = UpgradeState.LOCKED;

            if (i == 0)
            {
                archerUpgradeStates[i] = UpgradeState.UNLOCKED;
            }
        }
        
        for (int i = 0; i < mageUpgradeStates.Length; i++)
        {
            mageUpgradeStates[i] = UpgradeState.LOCKED;

            if (i == 0)
            {
                mageUpgradeStates[i] = UpgradeState.UNLOCKED;
            }
        }
        
        for (int i = 0; i < bombUpgradeStates.Length; i++)
        {
            bombUpgradeStates[i] = UpgradeState.LOCKED;

            if (i == 0)
            {
                bombUpgradeStates[i] = UpgradeState.UNLOCKED;
            }
        }
        
        for (int i = 0; i < slowdownUpgradeStates.Length; i++)
        {
            slowdownUpgradeStates[i] = UpgradeState.LOCKED;

            if (i == 0)
            {
                slowdownUpgradeStates[i] = UpgradeState.UNLOCKED;
            }
        }
        
        for (int i = 0; i < meteorUpgradeStates.Length; i++)
        {
            meteorUpgradeStates[i] = UpgradeState.LOCKED;

            if (i == 0)
            {
                meteorUpgradeStates[i] = UpgradeState.UNLOCKED;
            }
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
       
            archerUpgradeStates = data.archerUpgradeStates;
            mageUpgradeStates = data.mageUpgradeStates;
            bombUpgradeStates = data.bombUpgradeStates;
            slowdownUpgradeStates = data.slowdownUpgradeStates;
            meteorUpgradeStates = data.meteorUpgradeStates;
        
            _archerRangeIncrease = data._archerRangeIncrease;
            _archerDamageIncrease = data._archerDamageIncrease;

            _mageRangeIncrease = data._mageRangeIncrease;
            _mageDamageIncrease = data._mageDamageIncrease;

            _slowdownIncrease = data._slowdownIncrease;
            _slowdownRangeIncrease = data._slowdownRangeIncrease;

            _bombDamageIncrease = data._bombDamageIncrease;
            _bombRangeIncrease = data._bombRangeIncrease;
            _bombCostDecrease = data._bombCostDecrease;

            _meteorDamageIncrease = data._meteorDamageIncrease;
            _meteorRangeIncrease = data._meteorRangeIncrease;
            _meteorCountDownDecrease = data._meteorCountDownDecrease;
            
            
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