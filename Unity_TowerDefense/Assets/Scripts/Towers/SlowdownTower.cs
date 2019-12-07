using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownTower : Tower
{
    private List<RoadTile> roadTiles = new List<RoadTile>();
    private float speedMultiplayer;
    
    public override void Init(TowerSO towerData)
    {
        speedMultiplayer = towerData.speedMultiplayer;
        base.Init(towerData);
    }

    void Start()
    {
        Collider[] n = Physics.OverlapSphere(transform.position, range , LayerMask.GetMask("Road"));

        foreach (Collider neighbour in n)
        {
            RoadTile road = neighbour.GetComponent<RoadTile>();
            road.speedMultiplayer = speedMultiplayer;
            roadTiles.Add(road);
        }
    }

    private void OnDestroy()
    {
        foreach (var roadTile in roadTiles)
        {
            roadTile.speedMultiplayer = 1;
        }
    }
}
