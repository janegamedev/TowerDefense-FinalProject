using UnityEngine;

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
        _destination = position;
        reachedDestination = false;
    }

    public float GetVelocity()
    {
        return _characterController.velocity.magnitude;
    }

    private void Update()
    {
        if (!IsAtDestination())
        {
            reachedDestination = false;
            Vector3 offset = (_destination - transform.position).normalized * movementSpeed;
            _characterController.Move(offset * Time.deltaTime);
            transform.LookAt(_destination);
        }
        else
        {
            reachedDestination = true;
        }
        
        _animator.SetFloat("speed", _characterController.velocity.magnitude);
    }

    private bool IsAtDestination()
    {
        if(_destination != Vector3.zero)
            return Vector3.Distance(transform.position, _destination) <= stopDistance;
        else
        {
            return false;
        }
    }
}
