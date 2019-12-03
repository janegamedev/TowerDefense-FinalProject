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
        PlayerStats.Instance.ChangeCoinsAmount((int)(-tower.buildCost * 0.5));
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
        PlayerStats.Instance.ChangeCoinsAmount(towerData.buildCost);
        tower.Init(towerData);

        tower.transform.position = transform.position;
        isAvailable = false;
    }
}
