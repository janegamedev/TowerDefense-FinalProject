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
                        selectionPanel.cell = selectedTile.transform;
                        
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
                        selectionPanel.cell = selectedTile.transform;
                        
                        selectionPanel.buttons[0].onClick.AddListener(UpgradeTower);
                        selectionPanel.costs[0].text = towers[0].nextUpgrade.buildCost.ToString();
                        selectionPanel.buttons[1].onClick.AddListener(SellTower);
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
        selectedTile = null;
        ClosePanel();
        
        //Decrease money
        //GameManager.Instance.Currency += (int)(selectedCell.tower.buildCost * 0.7f);
    }

    private void UpgradeTower()
    {
        selectedTile.UpgradeTower();
        selectedTile = null;
        ClosePanel();
        
        //GameManager.Instance.Currency -= selectedCell.tower.nextUpgrade.buildCost;
    }

    private void SpawnTower(int id)
    {
        Debug.Log("here");
        selectedTile.PlaceTower(towers[id]);
        selectedTile = null;
        ClosePanel();
        
        //GameManager.Instance.Currency -= towers[id].buildCost;
    }
}
