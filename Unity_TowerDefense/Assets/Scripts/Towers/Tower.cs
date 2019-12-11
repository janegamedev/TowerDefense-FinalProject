using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Tower : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private protected LayerMask enemyLayerMask;
    private TowerType _type;

    private int _level;

    private int _buildCost;
    public int BuildCost => _buildCost;

    private protected float range;
    public TowerSO currentTower;
    public TowerSO _nextUpgrade;

    [SerializeField] public GameObject rangeDome;

    private InGameUi _inGameUi;

    private void Start()
    {
        _inGameUi = FindObjectOfType<InGameUi>();
    }

    public virtual void Init(TowerSO towerData)
    {
        currentTower = towerData;
        enemyLayerMask = LayerMask.GetMask("Enemy");
        _type = towerData.type;
        _level = towerData.level;
        _buildCost = towerData.buildCost;
        range = towerData.range;

        _nextUpgrade = towerData.nextUpgrade;
    }
    
    public void EnableDome()
    {
        rangeDome.SetActive(true);
    }

    public void DisableDome()
    {
        rangeDome.SetActive(false);
    }

    public TowerSO GetNextUpdate()
    {
        return _nextUpgrade;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Here");
        _inGameUi.ShowTowerDescription(currentTower);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _inGameUi.CloseTowerPanel();
    }
}

public interface IDealRangedDamage
{
    float Damage {get;set;}
    float AttackRate  {get;set;}
    
    GameObject[] Projectiles { get; set; }

    void Attack(Transform enemy);
}

public interface IHaveWarrior
{
    int Damage {get;set;}
    float AttackRate  {get;set;}
    
    List<GameObject> WarriorsAlive { get; set; }
    
    void SpawnWarrior();
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
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, range);
    }
}