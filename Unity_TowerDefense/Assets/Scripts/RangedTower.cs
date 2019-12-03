using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RangedTower : Tower, IDealRangedDamage
{
    public int Damage { get; set; }
    public float AttackRate { get; set; }
    public GameObject[] Projectiles { get; set; }
    
    public Transform weaponBody;
    public Transform[] firePoints;

    public override void Init(TowerSO towerData)
    {
        base.Init(towerData);
        
        Damage = towerData.damage;
        AttackRate = towerData.attackRate;
        Projectiles = towerData.projectiles;
    }


    private void Update()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, range, enemyLayerMask); 
        
        if (enemiesInRange.Length > 0)
        {
            if (!enemyToAttack || !enemiesInRange.Contains(enemyToAttack.GetComponent<Collider>()))
            {
                enemyToAttack = enemiesInRange[Random.Range(0, enemiesInRange.Length)].transform;
            }
            
            Vector3 direction = enemyToAttack.position - weaponBody.position;
            Quaternion toRotation = Quaternion.FromToRotation(weaponBody.forward, direction);
            weaponBody.rotation = Quaternion.Lerp(weaponBody.rotation, toRotation, 0.02f * Time.time);
            
            Attack(enemyToAttack);
        }
        else
        {
            enemyToAttack = null;
        }
    }

    public void Attack(Transform enemy)
    {
        if (Time.time > nextAttackTime)
        {
            nextAttackTime = Time.time + AttackRate;

            for (int i = 0; i < Projectiles.Length; i++)
            {
                Projectile spawnedProjectile = Instantiate(Projectiles[i], firePoints[i].position, firePoints[i].rotation).GetComponent<Projectile>();
                spawnedProjectile.SetTarget(enemyToAttack);
                spawnedProjectile.damage = Damage;
            }
        }
    }
    
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, range);
    }
}