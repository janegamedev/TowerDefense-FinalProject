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

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distance = speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position)<= 1)
        {
            HitEnemy();
            return;
        }
        
        transform.Translate(direction.normalized * distance, Space.World);
        transform.LookAt(target);
    }

    public virtual void HitEnemy()
    {
        Destroy(gameObject);
    }
}