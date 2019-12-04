using UnityEngine;

public class LocalProjectile : Projectile
{
    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distance = speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position)<= 1)
        {
            HitEnemy();
            return;
        }
        
        transform.Translate(direction.normalized * distance, Space.World);
        transform.LookAt(target);
    }
    
    public override void HitEnemy()
    {
        target.GetComponentInParent<Enemy>().TakeHit(damage, type);
        
        base.HitEnemy();
    }
}