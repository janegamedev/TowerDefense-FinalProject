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
    
    private float maxHealth;
    private float health;
    public float Health => health;
    
    private float _armour;
    private float _magicResistance;
    private float _speedMultiplayer;
    
    public float movementSpeed = 1f;
    private float _stopDistance;
    
    private int _bounty;
    public int Bounty => _bounty;

    private RoadTile _currentTile;
    
    private bool _isDead;
    
    public event System.Action<Enemy> OnDeath;
    
    private HealthBar _healthBar;
    
    private Vector3 _destination;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    
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
        
        _armour = characterData.armour;
        _magicResistance = characterData.magicResistance;

        movementSpeed = characterData.speed;
        _navMeshAgent.speed = characterData.speed;
        
        _bounty = characterData.bounty;
        _currentTile = startTile;

        Vector2 rp = Random.insideUnitCircle * (_stopDistance-1);
        _destination = startTile.transform.position + new Vector3(rp.x, startTile.transform.position.y , rp.y);
        _stopDistance = 3;
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
                        _destination = _currentTile.transform.position + Random.insideUnitSphere * (_stopDistance - 1);
                        _destination.y = transform.position.y;
                            
                        NavMeshHit hit;
                        if (NavMesh.SamplePosition(_destination, out hit, 1.0f, NavMesh.AllAreas)) 
                        {
                            _destination = hit.position;
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

    private void SetDestination()
    {
        _navMeshAgent.SetDestination(_destination);
        
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10, LayerMask.GetMask("Road")))
        {
            RoadTile road = hit.collider.GetComponent<RoadTile>();
            if (road)
            {
                if (_speedMultiplayer != road.speedMultiplayer)
                {
                    _speedMultiplayer = road.speedMultiplayer;
                    _navMeshAgent.speed *= road.speedMultiplayer;
                }
            }
        }
    }

    private void UpdateAnimation()
    {
        _animator.SetFloat("velocity", _navMeshAgent.velocity.magnitude);
    }


    private bool IsAtDestination()
    {
        if(_navMeshAgent.destination != Vector3.zero)
            return Vector3.Distance(transform.position, _destination) <= _stopDistance;
        else
        {
            return true;
        }
    }
    
    private void Die()
    {
        if (!_isDead)
        {
            _isDead = true;
            if (health <= 0)
            {
                
            }
            
            _destination = transform.position;
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
        /*_healthBar.UpdateHealth(health, maxHealth);*/
        
        if (health <= 0)
        {
            characterState = CharacterState.DEAD;
        }
    }
}