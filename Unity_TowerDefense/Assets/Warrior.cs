using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class Warrior : MonoBehaviour
{
    public enum State
    {
        RESTING,
        RUNNING,
        ATTACKING,
        DEAD
    }
    
    public event System.Action <GameObject> OnDeath;

    public State warriorState;
    public DamageType damageType;
    public Vector3 restPosition;
    public int lookingRange;
    public int stoppingDistance;
    public int attackRate;
    public int damage;
    public float maxHealth;
    public float health;
    public int movementSpeed;
    public Enemy enemyToAttack;
    public float nextAttackTime;
    public bool dead;

    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Init(Vector3 pos)
    {
        restPosition = restPosition;
    }

    private void Update()
    { 
        switch (warriorState)
        {
            case State.RESTING:
                MoveTowards(restPosition);
                CheckForEnemy();
                break;
            
            case State.RUNNING:
                MoveTowards(enemyToAttack.transform.position);
                break;
            
            case State.ATTACKING:
                MoveTowards(enemyToAttack.transform.position);
                AttackEnemy();
                break;
            
            case State.DEAD:
                Dead();
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void ResetStats()
    {
        health = maxHealth;
        enemyToAttack = null;
        warriorState = State.RESTING;
        dead = false;
    }

    void MoveTowards(Vector3 d)
    {
        Vector3 offset = d - transform.position;

        if (offset.magnitude > stoppingDistance)
        {
            offset = offset.normalized * movementSpeed;
            _characterController.Move(offset * Time.deltaTime);
            transform.LookAt(d);
        }
        
        warriorState = enemyToAttack ? State.ATTACKING : State.RESTING;
    }
    void CheckForEnemy()
    {
        Collider[] n = Physics.OverlapSphere(transform.position, lookingRange , LayerMask.GetMask("Enemy"));

        if (n.Length > 0)
        {
            Debug.Log(n.Length);
            foreach (var enemy in n)
            {
                Enemy e = enemy.GetComponent<Enemy>();
                if (e.warriorToAttack == null || e.warriorToAttack == this)
                {
                    e.warriorToAttack = this;
                    enemyToAttack = e;
                    warriorState = State.RUNNING;
                    break;
                }
            }
        }
    }

    void AttackEnemy()
    {
        if (enemyToAttack)
        {
            if (Time.deltaTime > nextAttackTime)
            {
                nextAttackTime = Time.time + attackRate;
                enemyToAttack.TakeHit(damage, damageType);
            }
        }
        else
        {
            warriorState = State.RESTING;
        }
    }
    
    public void TakeHit(int damage)
    {
        health -= this.damage;

        if (health <= 0)
        {
            warriorState = State.DEAD;
        }
    }

    void Dead()
    {
        if (!dead)
        {
            dead = true;
            OnDeath?.Invoke(gameObject);
        }
    }
}
