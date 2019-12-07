using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] private TowerSO[] towers;
    [SerializeField] private LayerMask layerMask;
    
    [SerializeField] private GameObject towerSelectionPanel;
    [SerializeField] private GameObject towerUpgradePanel;
    
    private GameObject _currentPanel = null;
    private TowerTile _selectedTile = null;
    
    private Transform _canvas;
    private Camera _camera;

    private void Start()
    {
        _canvas = FindObjectOfType<InGameUi>().transform;
        _camera = Camera.main;
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameState previousState, GameState currentState)
    {
        if (currentState == GameState.PAUSED && _currentPanel!= null)
        {
            ClosePanel();
        }
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameState.RUNNING)
        {
            if (Input.GetMouseButtonDown(0) && _currentPanel == null)
            {
                RaycastHit hit;
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    _selectedTile = hit.collider.GetComponent<TowerTile>();
                    
                    if (_selectedTile.isAvailable)
                    {
                        ShowSelectionPanel();
                    }
                    else
                    {
                        ShowUpgradePanel();
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1) && _currentPanel != null)
            {
                ClosePanel();
            }
        }
    }

    private void ShowSelectionPanel()
    {
        _currentPanel = Instantiate(towerSelectionPanel, _canvas);
        SelectionPanel selectionPanel = _currentPanel.GetComponent<SelectionPanel>();
        selectionPanel.tile = _selectedTile;
                        
        for (int i = 0; i < selectionPanel.buttons.Length; i++)
        {
            int index = i;
            selectionPanel.buttons[i].onClick.AddListener(() => SpawnTower(index));
            selectionPanel.costs[i].text = towers[i].buildCost.ToString();
        }
    }

    private void ShowUpgradePanel()
    {
        _currentPanel = Instantiate(towerUpgradePanel, _canvas);
        SelectionPanel selectionPanel = _currentPanel.GetComponent<SelectionPanel>();
        selectionPanel.tile = _selectedTile;
        /*selectedTile.tower.EnableDome();*/
                        
        selectionPanel.buttons[0].onClick.AddListener(UpgradeTower);
        selectionPanel.costs[0].text = towers[0].nextUpgrade.buildCost.ToString();
        
        selectionPanel.buttons[1].onClick.AddListener(SellTower);
    }

    private void ClosePanel()
    {
/*        if (selectedTile.tower != null)
        {
            selectedTile.tower.DisableDome();
        }*/
        _selectedTile = null;

        SelectionPanel selectionPanel = _currentPanel.GetComponent<SelectionPanel>();

        foreach (var button in selectionPanel.buttons)
        {
            button.onClick.RemoveAllListeners();
        }

        Destroy(_currentPanel);
        _currentPanel = null;
    }

    private void SellTower()
    {
        _selectedTile.SellTower();
        ClosePanel();
    }

    private void UpgradeTower()
    {
        _selectedTile.UpgradeTower();
        ClosePanel();
    }

    private void SpawnTower(int id)
    {
        _selectedTile.PlaceTower(towers[id]);
        ClosePanel();
    }
}
