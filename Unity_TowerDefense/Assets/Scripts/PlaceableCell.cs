using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableCell : MonoBehaviour
{
    public bool isAvailable;
    public Tower tower;
    
    public void SellTower()
    {
        Destroy(tower.gameObject);
        tower = null;
        isAvailable = true;
    }

    public void UpgradeTower()
    {
        TowerSO nextTower = tower.nextUpgrade;
        
        Destroy(tower.gameObject);
        tower = null;
        
        PlaceTower(nextTower);
    }

    public void PlaceTower(TowerSO towerData)
    {
        tower = Instantiate(towerData.towerPrefab, transform).GetComponent<Tower>();
        tower.Init(towerData);

        tower.transform.position = transform.position;
        isAvailable = false;
    }
}
