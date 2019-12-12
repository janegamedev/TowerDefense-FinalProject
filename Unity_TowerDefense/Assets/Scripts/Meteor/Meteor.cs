using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] public GameObject explosionPrefab;
    
    private float _range;
    private float _damage;
    private bool _isExploded;

    public void SetValues(float range, float damage)
    {
        _range = range;
        _damage = damage;
    }

    //Check for trigger enter with tile road
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RoadTile>())
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (!_isExploded)
        {
            _isExploded = true;
            
            //Check for enemies in the range
            Collider[] enemies = Physics.OverlapSphere(transform.position, _range , LayerMask.GetMask("Enemy"));

            //Apply damage for collided enemies
            foreach (Collider enemy in enemies)
            {
                Enemy e = enemy.GetComponent<Enemy>();
                e.TakeHit(_damage, DamageType.PHYSICAL);
            }

            //Instantiate particle explotion
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
