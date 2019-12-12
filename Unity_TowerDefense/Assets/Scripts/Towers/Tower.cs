using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class Tower : MonoBehaviour
{
    [SerializeField] private GameObject rangeDome;
    [HideInInspector] public TowerSO currentTower;
    [HideInInspector] public TowerSO _nextUpgrade;
    
    private protected LayerMask enemyLayerMask;
    private TowerType _type;
    private protected float range;
    private int _buildCost;
    public int BuildCost => _buildCost;
    private InGameUi _inGameUi;
    
    private void Start()
    {
        _inGameUi = FindObjectOfType<InGameUi>();
    }

    //Init tower from Tower scriptable object
    public virtual void Init(TowerSO towerData)
    {
        currentTower = towerData;
        enemyLayerMask = LayerMask.GetMask("Enemy");
        _type = towerData.type;
        _buildCost = towerData.buildCost;
        range = towerData.range;

        _nextUpgrade = towerData.nextUpgrade;
    }
    
    public void EnableDome()
    {
        if (rangeDome != null)
        {
            rangeDome.SetActive(true);
        }
    }

    public void DisableDome()
    {
        if (rangeDome != null)
        {
            rangeDome.SetActive(false);
        }
    }

    public TowerSO GetNextUpdate()
    {
        return _nextUpgrade;
    }
}

public interface IDealRangedDamage
{
    float Damage {get;set;}
    float AttackRate  {get;set;}
    GameObject[] Projectiles { get; set; }
    void Attack(Transform enemy);
}


public class RangedTower : Tower, IDealRangedDamage
{
    public float Damage { get; set; }
    public float AttackRate { get; set; }
    public GameObject[] Projectiles { get; set; }
    
    [SerializeField] private Transform weaponBody;
    [SerializeField] private Transform[] firePoints;

    private Transform _enemyToAttack;
    private float _nextAttackTime;

    private void Update()
    {
        //Check for enemies in the range
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, range, enemyLayerMask);

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
}