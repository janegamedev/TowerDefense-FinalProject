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