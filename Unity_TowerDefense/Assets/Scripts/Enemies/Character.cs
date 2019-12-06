using System;
using UnityEngine;

public enum CharacterState
{
    RESTING,
    RUNNING,
    ATTACKING,
    DEAD
}

public class Character : MonoBehaviour
{
    private string _enemyName;
    private protected CharacterState characterState;
    private DamageType damageType;
    
    private protected float maxHealth;
    private protected float health;
    private int _damage;
    private float _attackRate;
    private float _armour;
    private float _magicResistance;
    private float _speed;
    
    private protected bool isDead;
    
    public event System.Action<GameObject> OnDeath;

    private protected CharacterNavigationController characterNavigationController;
    private HealthBar _healthBar;
    
    private protected Vector3 destination;
    private protected Character characterToAttack;

    private float _nextAttackTime;

    private void Awake()
    {
        characterNavigationController = GetComponent<CharacterNavigationController>();
        _healthBar = GetComponentInChildren<HealthBar>();
    }

    public virtual void Init(EnemySO characterData, RoadTile startTile)
    {
        SetDefaultState();
        _enemyName = characterData.enemyName;
        maxHealth = characterData.health;
        health = maxHealth;
        _damage = characterData.damage;
        _attackRate = characterData.attackRate;
        
        _armour = characterData.armour;
        _magicResistance = characterData.magicResistance;

        characterNavigationController.movementSpeed = characterData.speed;
        characterNavigationController.SetDestination(startTile.transform.position);
    }
    
    public virtual void Init(EnemySO characterData, Vector3 pos)
    {
        _enemyName = characterData.enemyName;
        maxHealth = characterData.health;
        health = maxHealth;
        _damage = characterData.damage;
        _attackRate = characterData.attackRate;
        
        _armour = characterData.armour;
        _magicResistance = characterData.magicResistance;

        characterNavigationController.movementSpeed = characterData.speed;
        characterNavigationController.SetDestination(pos);
    }

    protected void AttackEnemy()
    {
        if (characterToAttack)
        {
            _nextAttackTime = Time.time + _attackRate;
            
            if (Time.deltaTime >= _nextAttackTime)
            {
                characterToAttack.TakeHit(_damage, damageType);
            }
        }
        else
        {
            SetDefaultState();
        }
    }

    protected virtual void SetDefaultState()
    {
        
    }

    protected virtual void Die()
    {
        OnDeath?.Invoke(gameObject);
    }
    
    public void SetCharacterToAttack(Character target)
    {
        characterToAttack = target;
    }

    public Character GetCharacterToAttack()
    {
        return characterToAttack;
    }
    
    public void TakeHit(float amount, DamageType type)
    {
        switch (type)
        {
            case DamageType.PHYSICAL:
                health -= amount * (1 - _armour);
                break;
            
            case DamageType.MAGIC:
                health -= amount * (1 - _magicResistance);
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        
        _healthBar.UpdateHealth(health, maxHealth);
        
        if (health <= 0)
        {
            characterState = CharacterState.DEAD;
        }
    }
}