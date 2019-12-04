using System.Collections;
using System.Collections.Generic;

public class TowerTile : Tile
{
    public bool isAvailable;
    public Tower tower;
    public bool isEnd;
    
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