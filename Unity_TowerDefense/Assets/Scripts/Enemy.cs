using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    
    public float health;
    public int damage;
    public float attackRate;
    public float armour;
    
    public float magicResistance;
    public float speed;
    public int bounty;
    
    public float startingHp;
    public bool dead;

    public event System.Action OnDeath;

    private CharacterNavigationController _characterNavigationController;
    private WaypointNavigator _waypointNavigator;

    private void Awake()
    {
        health = startingHp;
        _characterNavigationController = GetComponent<CharacterNavigationController>();
        _waypointNavigator = GetComponent<WaypointNavigator>();

        _waypointNavigator.OnDestroy += Die;
    }

    public void TakeHit(float amount, DamageType type)
    {
        switch (type)
        {
            case DamageType.PHYSICAL:
                health -= amount * (1 - armour);
                break;
            
            case DamageType.MAGIC:
                health -= amount * (1 - magicResistance);
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    private void Die()
    {
        if (health <= 0)
        {
            PlayerStats.Instance.GetBounty(bounty);
        }
        
        dead = true;
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    public void Init(EnemySO enemyData, RoadTile waypoint)
    {
        health = enemyData.health;
        damage = enemyData.damage;
        attackRate = enemyData.attackRate;
        
        armour = enemyData.armour;
        magicResistance = enemyData.magicResistance;

        _characterNavigationController.movementSpeed = enemyData.speed;
        bounty = enemyData.bounty;

        _waypointNavigator.currentWaypoint = waypoint;
        _characterNavigationController.SetDestination(waypoint.transform.position);
    }
}
