using UnityEngine;

public enum DamageType
{
    PHYSICAL,
    MAGIC
}
public class Projectile : MonoBehaviour
{
    public DamageType type;
    public int damage;
    public float speed; 
    public Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public virtual void HitEnemy()
    {
        Destroy(gameObject);
    }
}