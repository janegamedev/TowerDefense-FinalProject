using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startingHp;
    public float hp;
    public bool dead;

    public event System.Action OnDeath;

    private void Start()
    {
        hp = startingHp;
    }

    public void TakeHit(float damage)
    {
        hp -= damage;

        if (hp <= 0 && !dead)
        {
            Die();
        }
    }

    public void Die()
    {
        dead = true;
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
