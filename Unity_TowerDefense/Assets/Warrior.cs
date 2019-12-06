using System;
using UnityEngine;


public class Warrior : Character
{
    public Vector3 restPosition;
    public int lookingRange;

    public override void Init(EnemySO characterData, Vector3 pos)
    {
        restPosition = pos;
/*        lookingRange = enemyData.AttackRange;*/
    }

    private void Update()
    { 
        if (characterState != CharacterState.DEAD)
        {
            if (characterToAttack != null && characterNavigationController.reachedDestination)
            {
                characterState = CharacterState.ATTACKING;
            }
            else if(characterToAttack != null)
            {
                characterState = CharacterState.RUNNING;
            }
            else
            {
                characterState = CharacterState.RESTING;
            }
        }

        switch (characterState)
        {
            case CharacterState.RESTING:
                
                destination = restPosition;
                characterNavigationController.SetDestination(destination);

                Collider[] n = Physics.OverlapSphere(transform.position, lookingRange , LayerMask.GetMask("Enemy"));

                if (n.Length > 0)
                {
                    foreach (var enemy in n)
                    {
                        Enemy e = enemy.GetComponent<Enemy>();
                        if (e.GetCharacterToAttack() == null || e.GetCharacterToAttack() == this)
                        {
                            e.SetCharacterToAttack(this);
                            characterToAttack = e;
                            characterState = CharacterState.RUNNING;
                            
                            break;
                        }
                    }
                }
                
                break;
            
            case CharacterState.RUNNING:
                destination = characterToAttack.transform.position;
                characterNavigationController.SetDestination(destination);
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
        characterState = CharacterState.RESTING;
    }

    public void ResetStats()
    {
        isDead = false;
        health = maxHealth;
        characterToAttack = null;
        SetDefaultState();
    }

    protected override void Die()
    {
        if (!isDead)
        {
            isDead = true;
            base.Die();
        }
    }
}
