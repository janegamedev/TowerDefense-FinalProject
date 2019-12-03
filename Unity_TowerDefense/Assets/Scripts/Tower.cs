using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerType type;
    public LayerMask enemyLayerMask;

    public int level;
    public int buildCost;
    public int range;

    public GameObject rangeObject;
    public TowerSO nextUpgrade;

    public Transform enemyToAttack;
    public float nextAttackTime;

    public virtual void Init(TowerSO towerData)
    {
        type = towerData.type;
        level = towerData.level;
        buildCost = towerData.buildCost;
        range = towerData.range;

        nextUpgrade = towerData.nextUpgrade;
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
    
    int WarriorHealth { get; set; }
    
    int WarriorAmount { get; set; }
    
    
    void SpawnWarrior();
}