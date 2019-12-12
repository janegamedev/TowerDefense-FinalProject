using UnityEngine;

public class LocalProjectile : Projectile
{
    private Vector3 _previousTargetPos;
    
    private void Update()
    {
        Vector3 targetPos;
        Vector3 direction;

        if (target == null)
        {
            targetPos = _previousTargetPos;
            direction = targetPos - transform.position;

            if (direction.magnitude <= 1)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            //Update direction of movement
            targetPos = new Vector3(target.position.x, target.GetComponentInChildren<Renderer>().bounds.size.y / 2, target.position.z);
            _previousTargetPos = targetPos;
            direction = targetPos - transform.position;
        }

        float distance = speed * Time.deltaTime;

        RaycastHit[] hits = Physics.RaycastAll(new Ray(transform.position, direction.normalized), distance,
            LayerMask.GetMask("Enemy"));

        foreach (var hit in hits)
        {
            hit.collider.GetComponent<Enemy>().TakeHit(damage, type);
            HitEnemy();
            break;
        }
        
        //Move and rotate projectile
        transform.Translate(direction.normalized * distance, Space.World);
        transform.LookAt(targetPos);
    }
}