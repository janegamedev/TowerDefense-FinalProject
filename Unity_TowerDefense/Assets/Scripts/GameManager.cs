using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
    {
        MENU,
        SELECTION,
        RUNNING,
        PAUSED,
        END
    }
    
public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public Events.EventGameState OnGameStateChanged;             //Event on change game state
    
    public string menuSceneName;
    public string levelSelectionSceneName;
    public string levelSceneName;
    
    public GameObject[] systemPrefabs;                                             //List of the managers need to instantiate
    private List<GameObject> _instancedSystemPrefabs = new List<GameObject>();     //List of instances managers
    
    [SerializeField] private GameState currentGameState = GameState.MENU;          //Current game state
    public GameState CurrentGameState 
    {
        get => currentGameState;
    }

    private string _path;
    private ProgressSceneLoader _progressSceneLoader;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        InstantiateSystemPrefabs();

        _progressSceneLoader = FindObjectOfType<ProgressSceneLoader>();
        _progressSceneLoader.OnSceneLoadedCompleted.AddListener(OnLoadCompleted);
        
        DontDestroyOnLoad(gameObject);
        
        UpdateState(currentGameState);
    }
    
    private void InstantiateSystemPrefabs()
    {
        foreach (var prefab in systemPrefabs)
        {
            var go = Instantiate(prefab);
            _instancedSystemPrefabs.Add(go);
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentGameState == GameState.RUNNING || currentGameState == GameState.PAUSED)
            {
                TogglePause();
            }
        }
    }

    private void UpdateState(GameState state)
    {
        GameState previousGameState = currentGameState;
        currentGameState = state;

        switch (currentGameState)
        {
            case GameState.MENU:
                Time.timeScale = 1f;
                break;
                
            case GameState.SELECTION:
                Time.timeScale = 1f;
                break;
            
            case GameState.RUNNING:
                Time.timeScale = 1f;
                break;
            
            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;
            
            case GameState.END:
                Time.timeScale = 0.0f;
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        OnGameStateChanged.Invoke(previousGameState, currentGameState);
    }
    
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void TogglePause()
    {
        UpdateState(currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        foreach (var prefab in _instancedSystemPrefabs)
        {
            Destroy(prefab);
        }
        
        _instancedSystemPrefabs.Clear();
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        UpdateState(GameState.END);
    }

    public void LoadLevelSelection(string path)
    {
        _path = path;
        _progressSceneLoader.LoadScene(levelSelectionSceneName);
        UpdateState(GameState.SELECTION);
    }

    private void OnLoadCompleted()
    {
        switch (CurrentGameState)
        {
            case GameState.MENU:
                break;
            case GameState.SELECTION:
                FindObjectOfType<Game>().SetPath(_path);
                break;
            
            case GameState.RUNNING:
                
                break;
            case GameState.PAUSED:
                break;
            case GameState.END:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    [ContextMenu("Main menu")]
    public void LoadMainMenu()
    {
        _progressSceneLoader.LoadScene(menuSceneName);
        UpdateState(GameState.MENU);
    }

    public void LoadLevel(LevelSO levelData)
    {
        Debug.Log("Loading level " + levelData.levelName);
        /*_progressSceneLoader.LoadScene(levelSceneName);
        //level data
        UpdateState(GameState.RUNNING);*/
    }
}
