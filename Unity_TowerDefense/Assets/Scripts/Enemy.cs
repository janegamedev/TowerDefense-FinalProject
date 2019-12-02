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
    public float hp;
    public bool dead;

    public event System.Action OnDeath;

    private CharacterNavigationController _characterNavigationController;
    private WaypointNavigator _waypointNavigator;

    private void Awake()
    {
        hp = startingHp;
        _characterNavigationController = GetComponent<CharacterNavigationController>();
        _waypointNavigator = GetComponent<WaypointNavigator>();

        _waypointNavigator.OnDestroy += Die;
    }

    public void TakeHit(float damage)
    {
        hp -= damage;

        if (hp <= 0 && !dead)
        {
            Die();
        }
    }

    private void Die()
    {
        dead = true;
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    public void Init(EnemySO enemyData, Waypoint waypoint)
    {
        health = enemyData.health;
        damage = enemyData.damage;
        attackRate = enemyData.attackRate;
        
        armour = enemyData.armour;
        magicResistance = enemyData.magicResistance;

        _characterNavigationController.movementSpeed = enemyData.speed;
        bounty = enemyData.bounty;

        _waypointNavigator.currentWaypoint = waypoint;
        _characterNavigationController.SetDestination(waypoint.GetPosition());
    }
}
