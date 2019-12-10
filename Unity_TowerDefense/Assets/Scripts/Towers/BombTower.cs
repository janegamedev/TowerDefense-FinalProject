public class BombTower : RangedTower
{
    public override void Init(TowerSO towerData)
    {
        base.Init(towerData);
        
        //Update player upgrades for range
        range *= (1 + Game.Instance._bombRangeIncrease);
        
        Damage = towerData.damage;
        //Update player upgrades for damage
        Damage *= (1 + Game.Instance._bombDamageIncrease);
        
        AttackRate = towerData.attackRate;
        Projectiles = towerData.projectiles;
    }
}