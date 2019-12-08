using System;
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

        Vector3 targetPos = new Vector3(target.position.x, target.GetComponentInChildren<Renderer>().bounds.size.y / 2,
            target.position.z);

        Vector3 direction = targetPos - transform.position;
        float distance = speed * Time.deltaTime;

        RaycastHit[] hits = Physics.RaycastAll(new Ray(transform.position, direction.normalized), distance,
            LayerMask.GetMask("Enemy"));

        foreach (var hit in hits)
        {
            hit.collider.GetComponent<Enemy>().TakeHit(damage, type);
            HitEnemy();
            break;
        }

        transform.Translate(direction.normalized * distance, Space.World);
        transform.LookAt(targetPos);
    }
}