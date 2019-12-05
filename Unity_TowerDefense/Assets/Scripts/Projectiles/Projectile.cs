using UnityEngine;

public enum DamageType
{
    PHYSICAL,
    MAGIC
}
public class Projectile : MonoBehaviour
{
    private protected DamageType type;
    private protected int damage;
    public int Damage { get=> damage; set=> damage=value; }
    [SerializeField] private protected float speed; 
    private protected Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private protected virtual void HitEnemy()
    {
        Destroy(gameObject);
    }
}