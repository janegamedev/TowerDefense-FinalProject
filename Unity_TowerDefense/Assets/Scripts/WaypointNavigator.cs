using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    private CharacterNavigationController _controller;
    public Waypoint currentWaypoint;
    
    public event System.Action OnDestroy;

    private void Awake()
    {
        _controller = GetComponent<CharacterNavigationController>();
    }

    private void Start()
    {
        _controller.SetDestination(currentWaypoint.GetPosition());
    }

    private void Update()
    {
        if (_controller.reachedDestination)
        {
            if (currentWaypoint.nextWaypoint != null)
            {
                currentWaypoint = currentWaypoint.nextWaypoint;
                _controller.SetDestination(currentWaypoint.GetPosition());
            }
            else
            {
                Die();
            }
        }
    }

    void Die()
    {
        OnDestroy?.Invoke();
        Destroy(gameObject);
    }
    
    
}
