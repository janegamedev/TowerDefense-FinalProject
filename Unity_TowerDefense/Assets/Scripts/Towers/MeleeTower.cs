using System;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTower : Tower, IHaveWarrior
{
    public int Damage { get; set; }
    public float AttackRate { get; set; }
    
    public GameObject warriorPrefab;
    public int warriorAmount;
    public List<GameObject> WarriorsAlive { get; set; }
    public Transform spawnPosition;


    public override void Init(TowerSO towerData)
    {
        base.Init(towerData);
        
        Damage = towerData.damage;
        AttackRate = towerData.attackRate;
    }

    private void Update()
    {
        if (WarriorsAlive.Count < warriorAmount)
        {
            for (int i = 0; i < WarriorsAlive.Count - warriorAmount; i++)
            {
                SpawnWarrior();
            }
        }
    }

    public void SpawnWarrior()
    {
        GameObject warrior = Instantiate(warriorPrefab, spawnPosition.position, Quaternion.identity);
        WarriorsAlive.Add(warrior);
    }

    public void DestroyWarrior(GameObject warrior)
    {
        WarriorsAlive.Remove(warrior);
        Destroy(warrior);
    }
}