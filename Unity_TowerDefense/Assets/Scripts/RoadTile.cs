using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class RoadTile : Tile
{
    public bool isEnd;
    public RoadTile nextTile;
    public bool isVisited;
    public Collider[] n;
    public float radios;
    
    public void PropagateRoad(RoadTile caller)
    {
        isVisited = true;
        
        if (caller != null)
        {
            nextTile = caller;
        }
        
        n = Physics.OverlapSphere(transform.position, radios , LayerMask.GetMask("Road"));
        
        foreach (Collider neighbour in n)
        {
            Debug.Log("asd");
            RoadTile road = neighbour.GetComponent<RoadTile>();
            if (road != null && !road.isVisited)
            {
                road.PropagateRoad(this);
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, radios);
    }
}