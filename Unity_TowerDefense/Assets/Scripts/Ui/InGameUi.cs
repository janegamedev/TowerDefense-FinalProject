using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUi : MonoBehaviour
{
    public LayerMask layerMask;
    //HUD
    [SerializeField] private TextMeshProUGUI coinsTmp;
    [SerializeField] private TextMeshProUGUI livesTmp;
    [SerializeField] private TextMeshProUGUI wavesTotalTmp;
    [SerializeField] private TextMeshProUGUI currentWaveTmp;
    
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private GameObject enemyPanel;
    [SerializeField] private Image enemyImage;
    [SerializeField] private Image shadeEnemyImage;
    [SerializeField] private TextMeshProUGUI enemyDescription;

    [SerializeField] private GameObject towerPanel;
    [SerializeField] private Image towerImage;
    [SerializeField] private Image shadeTowerImage;
    [SerializeField] private TextMeshProUGUI towerDescription;
    
    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private Image selectionImage;
    [SerializeField] private Image shadeSelectionImage;
    [SerializeField] private TextMeshProUGUI selectionDescription;
    
    private Enemy _selectedEnemy;
    private Tower _selectedTower;
    
    private Camera _camera;
    
    public void UpdateUi()
    {
        coinsTmp.text = PlayerStats.Instance.Coins.ToString();
        livesTmp.text = PlayerStats.Instance.Lives.ToString();
        wavesTotalTmp.text = PlayerStats.Instance.WavesTotal.ToString();
        currentWaveTmp.text = PlayerStats.Instance.CurrentWave.ToString();
    }
    
    private void Start()
    {
        _camera = Camera.main;
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameState previousState, GameState currentState)
    {
        settingsPanel.gameObject.SetActive(currentState == GameState.PAUSED);
        gameOverPanel.gameObject.SetActive(currentState == GameState.END);
    }

    public void OnSettingsClick()
    {
        GameManager.Instance.TogglePause();
    }
    
    public void RestartGame()
    {
        GameManager.Instance.LoadLevel(GameManager.Instance.currentLevelSo);
    }

    public void QuitToSelectionMenu()
    {
        GameManager.Instance.LoadLevelSelection(Game.Instance.Path);
    }

    private void ShowEnemyDescription(EnemySO enemy)
    {
        if (towerPanel.activeSelf)
        {
            CloseTowerPanel();
        }
        
        _selectedEnemy.SelectEnemy();
        
        enemyImage.sprite = enemy.enemyIcon;
        shadeEnemyImage.sprite = enemy.enemyIcon;
        enemyDescription.text = enemy.enemyDescription;
        
        enemyPanel.SetActive(true);
    }
    
    public void ShowTowerDescription(TowerSO tower)
    {
        if (enemyPanel.activeSelf)
        {
            CloseEnemyPanel();
        }
        
        towerImage.sprite = tower.towerImage;
        shadeTowerImage.sprite = tower.towerImage;
        towerDescription.text = tower.towerDescription;
        
        towerPanel.SetActive(true);
    }

    public void ShowSelection()
    {
        
    }

    public void CloseTowerPanel()
    {
        _selectedTower = null;
        towerPanel.SetActive(false);
    }

    private void CloseEnemyPanel()
    {
        if (_selectedEnemy != null)
        {
            _selectedEnemy.UnselectEnemy();
            _selectedEnemy = null;
        }
        
        enemyPanel.SetActive(false);
    }
    

    private void Update()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.GetComponent<Enemy>())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _selectedEnemy = hit.collider.GetComponent<Enemy>();

                    ShowEnemyDescription(_selectedEnemy.enemySo);
                }
            }
            else
            {
                Debug.Log("here");
                if (_selectedTower == null || _selectedTower != hit.collider.GetComponent<Tower>())
                {
                    _selectedTower = hit.collider.GetComponent<Tower>();
                    ShowTowerDescription(_selectedTower.currentTower);
                }
            }
        }
        else
        {
            CloseTowerPanel();
        }

        if (Input.GetMouseButtonDown(0) && _selectedEnemy != null)
        {
            CloseEnemyPanel();
        }
    }
}