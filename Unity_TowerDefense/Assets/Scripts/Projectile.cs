using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed; 
    private Transform target;

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

        if (direction.magnitude <= distance)
        {
            HitEnemy();
            return;
        }
        
        transform.Translate(direction.normalized * distance, Space.World);
        transform.LookAt(target);
    }

    private void HitEnemy()
    {
        Debug.Log("Hit");
        Destroy(gameObject);
    }
}