using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerType type;

    public int level;
    public int buildCost;
    public int range;
    
    public TowerSO nextUpgrade;

    public virtual void Init(TowerSO towerData)
    {
        type = towerData.type;
        level = towerData.level;
        buildCost = towerData.buildCost;
        range = towerData.range;

        nextUpgrade = towerData.nextUpgrade;
    }
}

public class RangedTower : Tower, IDealDamage
{
    public int Damage { get; set; }
    public float AttackRate { get; set; }

    public override void Init(TowerSO towerData)
    {
        base.Init(towerData);
        
        Damage = towerData.damage;
        AttackRate = towerData.attackRate;
    }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }
}

public interface IDealDamage
{
    int Damage {get;set;}
    float AttackRate  {get;set;}
    
    void Attack();
}

public interface IHaveWarrior
{
    int Damage {get;set;}
    float AttackRate  {get;set;}
    
    int WarriorHealth { get; set; }
    
    int WarriorAmount { get; set; }
    
    
    void SpawnWarrior();
}

public class MeleeTower : Tower, IHaveWarrior
{
    public int Damage { get; set; }
    public float AttackRate { get; set; }
    
    public int WarriorHealth { get; set; }
    public int WarriorAmount { get; set; }


    public override void Init(TowerSO towerData)
    {
        base.Init(towerData);
        
        Damage = towerData.damage;
        AttackRate = towerData.attackRate;
    }
    
    public void SpawnWarrior()
    {
        throw new System.NotImplementedException();
    }

 
}
