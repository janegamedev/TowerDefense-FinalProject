using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableCell : MonoBehaviour
{
    public bool isAvailable;
    public GameObject tower;
    
    public void SellTower()
    {
        Destroy(tower);
        tower = null;
        isAvailable = true;
    }

    public void UpgradeTower()
    {
        /*TowerSO nextTower = tower.nextTower;
        Destroy(tower);
        PlaceTower(nextTower);*/
    }

    public void PlaceTower(TowerSO towerData)
    {
/*        tower = new GameObject(towerData.towerModel);*/
        tower.transform.position = transform.position;
        
        // init data selectedCell.tower.Init(towers[id]);
        
        isAvailable = false;
    }
}
