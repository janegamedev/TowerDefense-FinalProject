using System;
using System.Collections;
using UnityEngine;

public class AreaProjectile : Projectile
{
    [SerializeField] private int areaRange;
    [SerializeField] private float timeToExplore;
    [SerializeField] private GameObject explosionParticle;
    
    private Rigidbody _rigidbody;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Launch();
    }

    void Launch()
    {
        Vector3 Vo = CalculateVelocity(target.position, transform.position, 1f);
        transform.rotation = Quaternion.LookRotation(Vo);

        _rigidbody.velocity = Vo;
    }
    
    //Check for collision with enemy or road
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Tile>() || other.GetComponent<Enemy>())
        {
            HitEnemy();
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
        }
    }
    
    private protected override void HitEnemy()
    {
        Collider[] enemyInArea = Physics.OverlapSphere(transform.position, areaRange, LayerMask.GetMask("Enemy"));

        foreach (var enemy in enemyInArea)
        {
            enemy.GetComponentInParent<Enemy>().TakeHit(damage, type);
        }

        base.HitEnemy();
    }

    //Calculate trajectory for bomb 
    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }
}