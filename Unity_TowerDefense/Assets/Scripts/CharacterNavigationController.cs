using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavigationController : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;
    public float stopDistance;
    public Vector3 destination;
    public bool reachedDestination;

    public void SetDestination(Vector3 position)
    {
        destination = position;
        reachedDestination = false;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, destination) > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
            transform.LookAt(destination);
        }
        else
        {
            reachedDestination = true;
        }
    }
}
