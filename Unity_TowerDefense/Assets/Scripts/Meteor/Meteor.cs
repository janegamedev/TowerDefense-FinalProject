using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float range;
    public float damage;
    public bool isExploded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RoadTile>())
        {
            Explode();
        }
    }

    private void Explode()
    {
        Debug.Log("Here");
        if (!isExploded)
        {
            isExploded = true;
            Collider[] enemies = Physics.OverlapSphere(transform.position, range , LayerMask.GetMask("Enemy"));

            foreach (Collider enemy in enemies)
            {
                Enemy e = enemy.GetComponent<Enemy>();
                e.TakeHit(damage, DamageType.PHYSICAL);
            }
        
            Destroy(gameObject);
        }
    }
}
