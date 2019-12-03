using UnityEngine;

public class AreaProjectile : Projectile
{
    public int areaRange;
    
    public override void HitEnemy()
    {
        Collider[] enemyInArea = Physics.OverlapSphere(transform.position, areaRange, LayerMask.GetMask("Enemy"));

        foreach (var enemy in enemyInArea)
        {
            enemy.GetComponentInParent<Enemy>().TakeHit(damage, type);
        }

        base.HitEnemy();
    }
}