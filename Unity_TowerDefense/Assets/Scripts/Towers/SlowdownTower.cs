using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownTower : Tower
{
    private List<RoadTile> roadTiles = new List<RoadTile>();
    private float speedMultiplayer;
    
    //Init data from Tower scriptable object
    public override void Init(TowerSO towerData)
    {
        base.Init(towerData);
        
        //Update stats with player upgrades
        speedMultiplayer = towerData.speedMultiplier * (1 + Game.Instance._slowdownIncrease);
        range *= (1 + Game.Instance._slowdownRangeIncrease);
    }

    
    //Set all slowdown roads at the start
    void Start()
    {
        Collider[] n = Physics.OverlapSphere(transform.position, range , LayerMask.GetMask("Road"));

        foreach (Collider neighbour in n)
        {
            RoadTile road = neighbour.GetComponentInParent<RoadTile>();
            road.speedMultiplier = speedMultiplayer;
            road.ActivateSlowdownParticle();
            roadTiles.Add(road);
        }
    }

    //Reset speed multiplier on roadTiles
    private void OnDestroy()
    {
        foreach (var roadTile in roadTiles)
        {
            roadTile.speedMultiplier = 1;
            roadTile.DeactivateSlowdownParticle();
        }
    }
}
