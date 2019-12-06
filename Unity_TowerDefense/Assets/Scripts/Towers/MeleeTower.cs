using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeleeTower : Tower, IHaveWarrior
{
    public int Damage { get; set; }
    public float AttackRate { get; set; }
    public List<GameObject> WarriorsAlive { get; set; }
   
    [SerializeField] private GameObject warriorPrefab;
    [SerializeField] private int warriorAmount;
    [SerializeField] private float warriorRespawn;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private int restRadius;
    [SerializeField] private RoadTile tile;

    public override void Init(TowerSO towerData)
    {
        base.Init(towerData);
        
        Damage = towerData.damage;
        AttackRate = towerData.attackRate;
    }

    private void Start()
    {
        Invoke("SpawnWarrior", 1f);
    }

    public void SpawnWarrior()
    {
        Collider[] n = Physics.OverlapSphere(transform.position, range , LayerMask.GetMask("Road"));
        if (n.Length > 0)
        {
            tile = n[Random.Range(0, n.Length)].GetComponent<RoadTile>();
        }

        List<Vector3> restPos = GetRestPositions(tile);
        
        for (int i = 0; i < warriorAmount; i++)
        {
            Warrior warrior = Instantiate(warriorPrefab, spawnPosition.position, Quaternion.identity).GetComponent<Warrior>();
            warrior.OnDeath += DestroyWarrior;
            warrior.restPosition = restPos[i];
        }
    }

    
    List<Vector3> GetRestPositions(RoadTile rTile)
    {
        List<Vector3> positions = new List<Vector3>();
        
        Vector3 center = rTile.transform.position;
        Vector3 p1 = center + Vector3.forward * restRadius;

        float cos = Mathf.Cos(120 * Mathf.Deg2Rad) * restRadius;
        float sin = Mathf.Sin(120 * Mathf.Deg2Rad);
        
        var x2 = p1.x + cos;
        var x3 = p1.x -  cos;
        var z2 = p1.z - sin * restRadius;
        
        Vector3 p2 = new Vector3(x2, center.y, z2);
        Vector3 p3 = new Vector3(x3, center.y, z2);

        positions.Add(p1);
        positions.Add(p2);
        positions.Add(p3);
        
        return positions;
    }

    private void DestroyWarrior(GameObject warrior)
    {
        warrior.SetActive(false);
/*        warrior.transform.position = spawnPosition.position;

        StartCoroutine(Respawn(warrior));*/
    }

    IEnumerator Respawn(GameObject warrior)
    {
       yield return new  WaitForSeconds(warriorRespawn);
       
       warrior.SetActive(true);
       warrior.GetComponent<Warrior>().ResetStats();
    }
}