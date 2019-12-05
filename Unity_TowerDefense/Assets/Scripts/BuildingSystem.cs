using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] private TowerSO[] towers;
    [SerializeField] private LayerMask layerMask;
    
    public Transform canvasRoot;
    public GameObject towerPanel;
    public GameObject upgradePanel;
    public GameObject currentPanel;
    public TowerTile selectedTile;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        currentPanel = null;
        selectedTile = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentPanel == null)
            {
                RaycastHit hit;
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    selectedTile = hit.collider.GetComponent<TowerTile>();
                    
                    if (selectedTile.isAvailable)
                    {
                        currentPanel = Instantiate(towerPanel, canvasRoot);
                        SelectionPanel selectionPanel = currentPanel.GetComponent<SelectionPanel>();
                        selectionPanel.tile = selectedTile;
                        
                        for (int i = 0; i < selectionPanel.buttons.Length; i++)
                        {
                            int index = i;
                            selectionPanel.buttons[i].onClick.AddListener(() => SpawnTower(index));
                            selectionPanel.costs[i].text = towers[i].buildCost.ToString();
                        }
                    }
                    else
                    {
                        currentPanel = Instantiate(upgradePanel, canvasRoot);
                        SelectionPanel selectionPanel = currentPanel.GetComponent<SelectionPanel>();
                        selectionPanel.tile = selectedTile;
                        /*selectedTile.tower.EnableDome();*/
                        
                        selectionPanel.buttons[0].onClick.AddListener(UpgradeTower);
                        selectionPanel.costs[0].text = towers[0].nextUpgrade.buildCost.ToString();
                        selectionPanel.buttons[1].onClick.AddListener(SellTower);
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1) && currentPanel != null)
        {
            ClosePanel();
        }
    }

    private void ClosePanel()
    {
/*        if (selectedTile.tower != null)
        {
            selectedTile.tower.DisableDome();
        }*/
        
        selectedTile = null;

        SelectionPanel selectionPanel = currentPanel.GetComponent<SelectionPanel>();

        foreach (var button in selectionPanel.buttons)
        {
            button.onClick.RemoveAllListeners();
        }

        Destroy(currentPanel);
        currentPanel = null;
    }

    private void SellTower()
    {
        selectedTile.SellTower();
        ClosePanel();
    }

    private void UpgradeTower()
    {
        selectedTile.UpgradeTower();
        ClosePanel();
    }

    private void SpawnTower(int id)
    {
        selectedTile.PlaceTower(towers[id]);
        ClosePanel();
    }
}
