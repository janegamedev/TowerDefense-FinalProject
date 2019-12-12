using UnityEngine;

public class TowerTile : Tile
{
    [HideInInspector] public bool isAvailable;
    [HideInInspector] public Tower tower;

    private GameObject _particleSystem;

    private void Start()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>().gameObject;
        ActivateParticle();
    }

    public void SellTower()
    {
        Destroy(tower.gameObject);
        PlayerStats.Instance.ChangeCoinsAmount((int)(-tower.BuildCost * PlayerStats.Instance.SellPercentage));
        tower = null;
        isAvailable = true;
        ActivateParticle();
    }

    public void UpgradeTower()
    {
        TowerSO nextTower = tower.GetNextUpdate();
        
        Destroy(tower.gameObject);
        tower = null;
        
        PlaceTower(nextTower);
    }

    //Placing a tower
    public void PlaceTower(TowerSO towerData)
    {
        tower = Instantiate(towerData.towerPrefab, transform).GetComponent<Tower>();

        if (towerData.type == TowerType.ARTILLERY)
        {
            PlayerStats.Instance.ChangeCoinsAmount(towerData.buildCost * (int)(1 - Game.Instance._bombCostDecrease));
        }
        else
        {
            PlayerStats.Instance.ChangeCoinsAmount(towerData.buildCost);
        }
        tower.Init(towerData);

        tower.transform.position = transform.position;
        isAvailable = false;
        DeactivateParticle();
    }

    private void ActivateParticle()
    {
        _particleSystem.SetActive(true);
    }

    private void DeactivateParticle()
    {
        _particleSystem.SetActive(false);
    }
}