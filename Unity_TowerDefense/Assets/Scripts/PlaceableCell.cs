using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableCell : MonoBehaviour
{
    public bool isAvailable;
    public Tower tower;
    
    public void SellTower()
    {
        Destroy(tower);
        tower = null;
        isAvailable = true;
    }

    public void UpgradeTower()
    {
        TowerSO nextTower = tower.nextUpgrade;
        
        Destroy(tower);
        tower = null;
        
        PlaceTower(nextTower);
    }

    public void PlaceTower(TowerSO towerData)
    {
        if (towerData.type == TowerType.MELEE)
        {
            tower = Instantiate(towerData.towerPrefab, transform).AddComponent<MeleeTower>();
            tower.Init(towerData);
        }
        else
        {
            tower = Instantiate(towerData.towerPrefab, transform).AddComponent<RangedTower>();
            tower.Init(towerData);
        }

        tower.transform.position = transform.position;
        isAvailable = false;
    }
}
