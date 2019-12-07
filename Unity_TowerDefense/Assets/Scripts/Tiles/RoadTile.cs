using UnityEngine;

public class RoadTile : Tile
{
    public bool isEnd;
    public bool isStart;
    public RoadTile nextTile;
    public bool isVisited;
    public float r;

    public float speedMultiplayer;
    
    public void PropagateRoad(RoadTile caller)
    {
        isVisited = true;
        
        if (caller != null)
        {
            nextTile = caller;
        }
        
        Collider[] n = Physics.OverlapSphere(transform.position, r , LayerMask.GetMask("Road"));

        bool isPropagated = false;
        
        foreach (Collider neighbour in n)
        {
            RoadTile road = neighbour.GetComponent<RoadTile>();
            
            if (!road.isVisited)
            {
                road.PropagateRoad(this);
                isPropagated = true;
            }
        }

        if (!isPropagated)
        {
            isStart = true;
            Invoke("CallWave", 0.1f);
        }
    }

    public void CallWave()
    {
        FindObjectOfType<WavesManager>().Init();
    }
}