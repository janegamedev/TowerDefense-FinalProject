using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : Character
{
    private int _bounty;
    private RoadTile _currentTile;

    public override void Init(EnemySO characterData, RoadTile startTile)
    {
        _bounty = characterData.bounty;

        _currentTile = startTile;
        
        base.Init(characterData, startTile);
    }

    private void Update()
    {
        if (characterState != CharacterState.DEAD)
        {
            if (characterToAttack != null && characterNavigationController.reachedDestination)
            {
                characterState = CharacterState.ATTACKING;
            }
            else
            {
                characterState = CharacterState.RUNNING;
            }
        }
        
        switch (characterState)
        {
            case CharacterState.RUNNING:
                
                if (characterToAttack != null)
                {
                    destination = characterToAttack.transform.position;
                    characterNavigationController.SetDestination(destination);
                }
                else
                {
                    if (characterNavigationController.reachedDestination)
                    {
                        if (_currentTile != null)
                        {
                            _currentTile = _currentTile.nextTile;
                            destination = _currentTile.transform.position + Random.insideUnitSphere * 5;
                            destination.y = transform.position.y;
                            characterNavigationController.SetDestination(destination);
                        }
                        else
                        {
                            characterState = CharacterState.DEAD;
                        }
                    }
                    else
                    {
                        if (characterNavigationController.GetVelocity() > 0.2 && characterNavigationController.GetVelocity() <= 0.5f)
                        {
                            destination += Random.insideUnitSphere * 5;
                            destination.y = transform.position.y;
                            characterNavigationController.SetDestination(destination);
                        }
                    }
                }
                break;
            
            case CharacterState.ATTACKING:
                AttackEnemy();
                
                break;
            
            case CharacterState.DEAD:
                Die();
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override void SetDefaultState()
    {
        characterState = CharacterState.RUNNING;
    }

    protected override void Die()
    {
        if (!isDead)
        {
            isDead = true;
            if (health <= 0)
            {
                PlayerStats.Instance.GetBounty(_bounty);
            }
        
            PlayerStats.Instance.ChangeLives(1);
            
            base.Die();
        }
    }
}

