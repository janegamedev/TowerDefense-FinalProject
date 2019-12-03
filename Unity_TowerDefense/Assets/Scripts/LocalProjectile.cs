public class LocalProjectile : Projectile
{
    public override void HitEnemy()
    {
        target.GetComponentInParent<Enemy>().TakeHit(damage, type);
        
        base.HitEnemy();
    }
}