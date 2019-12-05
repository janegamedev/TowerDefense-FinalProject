using System;

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
            characterState = characterToAttack != null ? CharacterState.ATTACKING : CharacterState.RUNNING;
        }
        
        switch (characterState)
        {
            case CharacterState.RUNNING:
                if (characterNavigationController.reachedDestination)
                {
                    if (_currentTile != null)
                    {
                        _currentTile = _currentTile.nextTile;
                    }
                    else
                    {
                        characterState = CharacterState.DEAD;
                    }
                }
                
                destination = _currentTile.transform.position;
                break;
            
            case CharacterState.ATTACKING:
                if (!characterNavigationController.reachedDestination)
                {
                    destination = characterToAttack.transform.position;
                }
                else
                {
                    AttackEnemy();
                }
                break;
            case CharacterState.DEAD:
                Die();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        characterNavigationController.SetDestination(destination);
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

