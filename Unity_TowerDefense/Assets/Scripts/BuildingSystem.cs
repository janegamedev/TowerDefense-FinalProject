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
    public PlaceableCell selectedCell;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        currentPanel = null;
        selectedCell = null;
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
                    selectedCell = hit.collider.GetComponent<PlaceableCell>();
                    if (selectedCell.isAvailable)
                    {
                        currentPanel = Instantiate(towerPanel, canvasRoot);
                        currentPanel.transform.position = _camera.WorldToScreenPoint(selectedCell.transform.position);

                        SelectionPanel selectionPanel = currentPanel.GetComponent<SelectionPanel>();

                        for (int i = 0; i < towers.Length; i++)
                        {
                            selectionPanel.buttons[i].onClick.AddListener(() => SpawnTower(i));
                            /*selectionPanel.costs[i] = towers[i].buildCost;*/
                        }
                    }
                    else
                    {
                        currentPanel = Instantiate(upgradePanel, canvasRoot);
                        currentPanel.transform.position = _camera.WorldToScreenPoint(selectedCell.transform.position);

                        SelectionPanel selectionPanel = currentPanel.GetComponent<SelectionPanel>();
                        
                        selectionPanel.buttons[0].onClick.AddListener(SellTower);
                        /*selectionPanel.costs[0] = towers[0].upgradeCost;*/
                        selectionPanel.buttons[1].onClick.AddListener(UpgradeTower);
                        /*selectionPanel.costs[1] = towers[1].sell;*/
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
        selectedCell = null;
            
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
        selectedCell.SellTower();
        selectedCell = null;
        ClosePanel();
        
        //Decrease money
        //GameManager.Instance.Currency += (int)(selectedCell.tower.buildCost * 0.7f);
    }

    private void UpgradeTower()
    {
        selectedCell.UpgradeTower();
        selectedCell = null;
        ClosePanel();
        
        //GameManager.Instance.Currency -= selectedCell.tower.nextUpgrade.buildCost;
    }

    private void SpawnTower(int id)
    {
        selectedCell.PlaceTower(towers[id]);
        selectedCell = null;
        ClosePanel();
        
        //GameManager.Instance.Currency -= towers[id].buildCost;
    }
}
