public class ArcherTower : RangedTower
{
    public override void Init(TowerSO towerData)
    {
        base.Init(towerData);
        
        //Update player upgrades for range
        range *= (1 + Game.Instance._archerRangeIncrease);
        
        //Update player upgrades for damage
        Damage =  towerData.damage * (1 + Game.Instance._archerDamageIncrease);
        
        AttackRate = towerData.attackRate;
        Projectiles = towerData.projectiles;
    }
}