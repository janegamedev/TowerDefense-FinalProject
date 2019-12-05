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
        
        Vector3 targetPos = new Vector3(target.position.x, target.GetComponentInChildren<Renderer>().bounds.size.y/2 , target.position.z);

        Vector3 direction = targetPos - transform.position;
        float distance = speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPos)<= 1)
        {
            HitEnemy();
            return;
        }
        
        transform.Translate(direction.normalized * distance, Space.World);
        transform.LookAt(targetPos);
    }

    private protected override void HitEnemy()
    {
        target.GetComponentInParent<Enemy>().TakeHit(damage, type);
        
        base.HitEnemy();
    }
}