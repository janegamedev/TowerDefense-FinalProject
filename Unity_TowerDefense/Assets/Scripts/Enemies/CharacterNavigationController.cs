using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterNavigationController : MonoBehaviour
{
    public float movementSpeed = 1f;
    
    [SerializeField] private float stopDistance;
    
    private Vector3 _destination;
    public bool reachedDestination;

    private CharacterController _characterController;
    private Animator _animator;
    
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    public void SetDestination(Vector3 position)
    {
        _destination = position + Random.insideUnitSphere * 5;
        _destination.y = transform.position.y;
        reachedDestination = false;
    }

    private void Update()
    {
        MoveTowardsTarget();
        UpdateAnim();
    }

    void MoveTowardsTarget()
    {
        Vector3 offset = _destination - transform.position;

        if (offset.magnitude > 0.1f)
        {
            offset = offset.normalized * movementSpeed;
            _characterController.Move(offset * Time.deltaTime);
            transform.LookAt(_destination);
        }

        if (IsAtDestination())
        {
            reachedDestination = true;
        }
    }
    
    private bool IsAtDestination()
    {
        if(_destination != Vector3.zero)
            return Vector3.Distance(transform.position, _destination) < stopDistance;
        else
        {
            return false;
        }
    }

    void UpdateAnim()
    {
        _animator.SetFloat("speed", _characterController.velocity.magnitude);
    }
}
