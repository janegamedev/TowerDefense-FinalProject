using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private string _enemyName;
    
    private float _maxHealth;
    private float _health;
    private int _damage;
    private float _attackRate;
    private float _armour;
    
    private float _magicResistance;
    private float _speed;
    private int _bounty;
    
    private bool _dead;

    public Warrior warriorToAttack;

    public event System.Action OnDeath;

    private CharacterNavigationController _characterNavigationController;
    private CharacterNavigator _characterNavigator;
    private HealthBar _healthBar;

    private void Awake()
    {
        _characterNavigationController = GetComponent<CharacterNavigationController>();
        _characterNavigator = GetComponent<CharacterNavigator>();
        _healthBar = GetComponentInChildren<HealthBar>();

        _characterNavigator.OnDestroy += Die;
    }

    public void TakeHit(float amount, DamageType type)
    {
        switch (type)
        {
            case DamageType.PHYSICAL:
                _health -= amount * (1 - _armour);
                break;
            
            case DamageType.MAGIC:
                _health -= amount * (1 - _magicResistance);
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        
        _healthBar.UpdateHealth(_health, _maxHealth);
        
        if (_health <= 0 && !_dead)
        {
            Die();
        }
    }

    private void Die()
    {
        if (_health <= 0)
        {
            PlayerStats.Instance.GetBounty(_bounty);
        }
        
        _dead = true;
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    public void Init(EnemySO enemyData, RoadTile startTile)
    {
        _enemyName = enemyData.enemyName;
        _maxHealth = enemyData.health;
        _health = _maxHealth;
        _damage = enemyData.damage;
        _attackRate = enemyData.attackRate;
        
        _armour = enemyData.armour;
        _magicResistance = enemyData.magicResistance;

        _characterNavigationController.movementSpeed = enemyData.speed;
        _bounty = enemyData.bounty;

        _characterNavigator.CurrentTile = startTile;
        _characterNavigationController.SetDestination(startTile.transform.position);
    }
}
