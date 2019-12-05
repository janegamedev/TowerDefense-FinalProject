using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavigator : MonoBehaviour
{
    private CharacterNavigationController _controller;
    public RoadTile currentWaypoint;
    
    public event System.Action OnDestroy;

    private void Awake()
    {
        _controller = GetComponent<CharacterNavigationController>();
    }

    private void Update()
    {
        if (_controller.reachedDestination)
        {
            if (currentWaypoint.nextTile != null)
            {
                currentWaypoint = currentWaypoint.nextTile;
                _controller.SetDestination(currentWaypoint.transform.position);
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
        PlayerStats.Instance.ChangeLives(1);
        Destroy(gameObject);
    }
}
