using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private protected LayerMask enemyLayerMask;
    private TowerType _type;

    private int _level;

    private int _buildCost;
    public int BuildCost => _buildCost;

    private protected int range;
    private TowerSO _nextUpgrade;

    [SerializeField] public GameObject rangeDome;
    
    public virtual void Init(TowerSO towerData)
    {
        enemyLayerMask = LayerMask.GetMask("Enemy");
        _type = towerData.type;
        _level = towerData.level;
        _buildCost = towerData.buildCost;
        range = towerData.range;

        _nextUpgrade = towerData.nextUpgrade;
    }

    public void EnableDome()
    {
        rangeDome.SetActive(true);
    }

    public void DisableDome()
    {
        rangeDome.SetActive(false);
    }

    public TowerSO GetNextUpdate()
    {
        return _nextUpgrade;
    }
}

public interface IDealRangedDamage
{
    int Damage {get;set;}
    float AttackRate  {get;set;}
    
    GameObject[] Projectiles { get; set; }

    void Attack(Transform enemy);
}

public interface IHaveWarrior
{
    int Damage {get;set;}
    float AttackRate  {get;set;}
    
    List<GameObject> WarriorsAlive { get; set; }
    
    void SpawnWarrior();
}