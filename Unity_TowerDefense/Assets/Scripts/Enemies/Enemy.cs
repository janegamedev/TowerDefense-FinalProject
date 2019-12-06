using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum CharacterState
{
    ALIVE,
    DEAD
}

public class Enemy : MonoBehaviour
{
    private string _enemyName;
    public CharacterState characterState;
    private DamageType damageType;
    
    private protected float maxHealth;
    private protected float health;
    public float Health => health;

    private int _damage;
    private float _attackRate;
    private float _armour;
    private float _magicResistance;
    private float _speed;
    
    public float movementSpeed = 1f;
    private float _stopDistance;
    
    private int _bounty;
    public int Bounty => _bounty;

    private RoadTile _currentTile;
    
    private protected bool isDead;
    
    public event System.Action<Enemy> OnDeath;
    
    private HealthBar _healthBar;
    
    private protected Vector3 destination;
    private protected Enemy enemyToAttack;
    
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private float _nextAttackTime;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _healthBar = GetComponentInChildren<HealthBar>();
    }
    
    public void Init(EnemySO characterData, RoadTile startTile)
    {
        characterState = CharacterState.ALIVE;
        
        _enemyName = characterData.enemyName;
        maxHealth = characterData.health;
        health = maxHealth;
        _damage = characterData.damage;
        _attackRate = characterData.attackRate;
        
        _armour = characterData.armour;
        _magicResistance = characterData.magicResistance;

        _navMeshAgent.speed = characterData.speed;
        
        _bounty = characterData.bounty;
        _currentTile = startTile;

        Vector2 rp = Random.insideUnitCircle * 5;
        destination = startTile.transform.position + new Vector3(rp.x, startTile.transform.position.y , rp.y);
        _stopDistance = 5;
    }

    private void Update()
    {
        switch (characterState)
        {
            case CharacterState.ALIVE:
                
                if (IsAtDestination())
                {
                    if (_currentTile.nextTile != null)
                    {
                        _currentTile = _currentTile.nextTile;
                        destination = _currentTile.transform.position + Random.insideUnitSphere * 5;
                        destination.y = transform.position.y;
                            
                        NavMeshHit hit;
                        if (NavMesh.SamplePosition(destination, out hit, 1.0f, NavMesh.AllAreas)) 
                        {
                            destination = hit.position;
                        }
                        
                    }
                    else
                    {
                        characterState = CharacterState.DEAD;
                    }
                }
                
                SetDestination();
                UpdateAnimation();
                
                break;
            
            case CharacterState.DEAD:
                Die();
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private protected void SetDestination()
    {
        _navMeshAgent.SetDestination(destination);
    }

    private protected void UpdateAnimation()
    {
        _animator.SetFloat("velocity", _navMeshAgent.velocity.magnitude);
    }


    private protected bool IsAtDestination()
    {
        if(_navMeshAgent.destination != Vector3.zero)
            return Vector3.Distance(transform.position, destination) <= _stopDistance;
        else
        {
            return true;
        }
    }
    
    
    private protected bool NeedsDestination()
    {
        if (_navMeshAgent.destination == Vector3.zero)
        {
            return true;
        }

        var distance = Vector3.Distance(destination, _navMeshAgent.destination);
        if(distance <= _stopDistance)
        {
            return true;
        }

        return false;
    }

    

    protected virtual void Die()
    {
        if (!isDead)
        {
            isDead = true;
            if (health <= 0)
            {
                
            }
            
            destination = transform.position;
            SetDestination();
            GetComponent<Animator>().SetTrigger("dead");
        }
    }

    public void OnEndDeathAnimation()
    {
        OnDeath?.Invoke(this);
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