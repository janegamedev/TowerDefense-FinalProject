using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] private TowerSO[] towers;
    [SerializeField] private LayerMask layerMask;
    
    [SerializeField] private SelectionPanel towerSelectionPanel;
    [SerializeField] private SelectionPanel towerUpgradePanel;
    
    private TowerTile _selectedTile = null;

    private bool _isSelected;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameState previousState, GameState currentState)
    {
        if (currentState == GameState.PAUSED && _isSelected)
        {
            ClosePanel();
        }
    }

    //Input check and raycast for building system
    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameState.RUNNING)
        {
            if (Input.GetMouseButtonDown(0) && !_isSelected)
            {
                RaycastHit hit;
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    _selectedTile = hit.collider.GetComponentInParent<TowerTile>();

                    if (_selectedTile.isAvailable)
                    {
                        ShowSelectionPanel();
                    }
                    else
                    {
                        ShowUpgradePanel();
                    }

                    _isSelected = true;
                }
            }
            else if (Input.GetMouseButtonDown(1) && _isSelected)
            {
                ClosePanel();
            }
        }
    }
    
    private void ShowSelectionPanel()
    {
        towerSelectionPanel.gameObject.SetActive(true);
        towerSelectionPanel.Init(_selectedTile);
        
        for (int i = 0; i < towerSelectionPanel.buttons.Length; i++)
        {
            int index = i;
            towerSelectionPanel.buttons[i].onClick.AddListener(() => SpawnTower(index));
            towerSelectionPanel.costs[i].text = towers[i].buildCost.ToString();
        }
    }

    private void ShowUpgradePanel()
    {
        towerUpgradePanel.gameObject.SetActive(true);
        towerUpgradePanel.Init(_selectedTile);
        
        _selectedTile.tower.EnableDome();
                        
        towerUpgradePanel.buttons[0].onClick.AddListener(UpgradeTower);
        towerUpgradePanel.costs[0].text = _selectedTile.tower.GetNextUpdate().buildCost.ToString();
        
        towerUpgradePanel.buttons[1].onClick.AddListener(SellTower);
    }

    private void ClosePanel()
    {
        _selectedTile.tower.DisableDome();
        _isSelected = false;
        _selectedTile = null;

        towerSelectionPanel.gameObject.SetActive(false);
        towerUpgradePanel.gameObject.SetActive(false);
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
