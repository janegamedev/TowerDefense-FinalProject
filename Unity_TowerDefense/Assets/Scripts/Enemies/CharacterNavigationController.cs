using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Random = UnityEngine.Random;

public class CharacterNavigationController : MonoBehaviour
{
    public float movementSpeed = 1f;
    public float rotationSpeed;
    public float stopDistance;
    public Vector3 destination;
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
        destination = position + Random.insideUnitSphere * 5;
        destination.y = transform.position.y;
        reachedDestination = false;
    }

    private void Update()
    {
        MoveTowardsTarget();
        UpdateAnim();
    }

    void MoveTowardsTarget()
    {
        Vector3 offset = destination - transform.position;

        if (offset.magnitude > 0.1f)
        {
            offset = offset.normalized * movementSpeed;
            _characterController.Move(offset * Time.deltaTime);
            transform.LookAt(destination);
        }

        if (IsAtDestination())
        {
            reachedDestination = true;
        }
    }
    
    private bool IsAtDestination()
    {
        if(destination != Vector3.zero)
            return Vector3.Distance(transform.position, destination) < stopDistance;
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
