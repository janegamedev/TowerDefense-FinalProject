using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RangedTower : Tower, IDealRangedDamage
{
    public int Damage { get; set; }
    public float AttackRate { get; set; }
    public GameObject[] Projectiles { get; set; }
    
    [SerializeField] private Transform weaponBody;
    [SerializeField] private Transform[] firePoints;

    public Transform _enemyToAttack;
    private float _nextAttackTime;
    
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
        Debug.Log(range + " " + enemiesInRange.Length);
        
        if (enemiesInRange.Length > 0)
        {
            if (!_enemyToAttack || !enemiesInRange.Contains(_enemyToAttack.GetComponent<Collider>()))
            {
                _enemyToAttack = enemiesInRange[Random.Range(0, enemiesInRange.Length)].transform;
            }

            if (weaponBody != null)
            {
                Vector3 lookPos = _enemyToAttack.position - weaponBody.transform.position;
                lookPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                weaponBody.transform.rotation = Quaternion.Slerp(weaponBody.transform.rotation, rotation, Time.deltaTime * 2);
            }

            Attack(_enemyToAttack);
        }
        else
        {
            _enemyToAttack = null;
        }
    }

    public void Attack(Transform enemy)
    {
        if (Time.time > _nextAttackTime)
        {
            _nextAttackTime = Time.time + AttackRate;

            for (int i = 0; i < Projectiles.Length; i++)
            {
                Projectile spawnedProjectile = Instantiate(Projectiles[i], firePoints[i].position, firePoints[i].rotation).GetComponent<Projectile>();
                spawnedProjectile.SetTarget(_enemyToAttack);
                spawnedProjectile.Damage = Damage;
            }
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, range);
    }
}