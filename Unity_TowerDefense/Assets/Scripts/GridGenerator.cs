using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public Texture2D mapTexture;

    public Color32[] colors;
    public GameObject[] hexPrefabs;
    public Transform[] tileRoots;

    public Tile[,] grid;

    private float _hexW;
    private float _hexH;
    public Vector3 _startPosition = Vector3.zero;

    private RoadTile endTile;


    public GameObject[] trees;
/*    public GameObject[] trees;*/

    private void Start()
    {
        grid = new Tile[mapTexture.width, mapTexture.height];
        
        _hexW = hexPrefabs[0].GetComponent<Renderer>().bounds.size.x / 2;
        _hexH = hexPrefabs[0].GetComponent<Renderer>().bounds.size.z / 2;

        _startPosition.x = -_hexW * 1.5f * (mapTexture.width / 2);
        _startPosition.z =_hexH * 2 * (mapTexture.width / 2);

        CreateGrid();
        endTile = tileRoots[2].GetComponentsInChildren<RoadTile>().First(x => x.isEnd == true);
        Invoke("Overlap", 0.1f );
    }

    public void Overlap()
    {
        endTile.PropagateRoad(null);
    }

    void CreateGrid()
    {
        for (int y = 0; y < mapTexture.height; y++)
        {
            for (int x = 0; x < mapTexture.width; x++)
            {
                Vector2Int gridPos = new Vector2Int(x,y);

                int index = GetTileFromImage(x, y);
                Tile hex = Instantiate(hexPrefabs[index], tileRoots[index]).GetComponent<Tile>();
                hex.SetGridPosition(gridPos);
                hex.transform.localPosition = CalcWorldPos(gridPos);
                grid[x,y] = hex;
                hex.name = "Hexagon X: " + x + " Y: " + y;
            }
        }
    }

    void GenerateRoadWaypoints()
    {

        /*endTile.neighbourRoads = GetNeighboursRoad(endTile.GridPosition.x, endTile.GridPosition.y);*/
        
        for (int i = 0; i < tileRoots[2].childCount; i++)
        {
            RoadTile child = tileRoots[2].GetChild(i).GetComponent<RoadTile>();
            //child.neighbourRoads = GetNeighboursRoad(child);
        }
/*

        endTile.BackPropagation(null);*/
    }

    List<RoadTile> GetNeighboursRoad(RoadTile tile)
    {
        List<RoadTile> roadTiles = new List<RoadTile>();
        
        Collider[] neighbours = Physics.OverlapSphere(tile.transform.position, 50);
        
        if (neighbours.Length > 0)
        {
            Debug.Log("Here");
            foreach (var n in neighbours)
            {
                RoadTile road = n.GetComponent<RoadTile>();
                if (road)
                {
                    roadTiles.Add(road);
                }
            }
        }
        /*for (int i = 0; i < tileRoots[2].childCount; i++)
        {
            RoadTile road = tileRoots[2].GetChild(i).GetComponent<RoadTile>();
            
            if (road.GridPosition.x == x && road.GridPosition.y == y)
            {
                continue;
            }

            if (road.GridPosition.x >= x - 1 && road.GridPosition.x <= x + 1)
            {
                if (road.GridPosition.y >= y - 1 && road.GridPosition.y <= y + 1)
                {
                    roadTiles.Add(road);
                }
            }
        }*/
        /*if (y % 2 != 0 && x % 2 == 0)
        {
            if (x - 1 >= 0 && y - 1 >= 0)
            {
                neighbours.Add(grid[x - 1, y - 1 ]);
            }
        }
        else if(y % 2 == 0 && x % 2 != 0)
        {
            if (x - 1 >= 0)
            {
                neighbours.Add(grid[x - 1, y]);
            }
        }
        
        if (x - 1 >= 0 && y + 1 < mapTexture.height)
        {
            neighbours.Add(grid[x - 1, y + 1]);
        }

        if (y - 1 >= 0)
        {
            neighbours.Add(grid[x, y - 1]);
        }
        
        if (x + 1 < mapTexture.width)
        {
            neighbours.Add(grid[x + 1, y]);
            
            if (y + 1 < mapTexture.height)
            {
                neighbours.Add(grid[x + 1, y + 1]);
            }
        }

        if (y + 1 < mapTexture.height)
        {
            neighbours.Add(grid[x, y + 1]);
        }
        
        
        foreach (var neighbour in neighbours)
        { 
            RoadTile road = neighbour.GetComponent<RoadTile>();
            
            if (road && !road.isVisited)
            {
                roadTiles.Add(road);
            }
        }
*/

        return roadTiles;
    }
    
    int GetTileFromImage(int x, int y)
    {
        Color32 pixelColor = mapTexture.GetPixel(x, mapTexture.height - 1 - y);

        int index = 0; 
        for (int i = 0; i < colors.Length; i++)
        {
            if (colors[i].Equals(pixelColor))
            {
                index = i;
            }
        }
        
        return index;
    }
    
    Vector3 CalcWorldPos(Vector2Int gridPos)
    {
        float x = _startPosition.x + gridPos.x * (_hexW * 1.5f);
        float y = _startPosition.z - (gridPos.y * _hexH * 2) - (gridPos.x % 2 * _hexH);
        
        return new Vector3(x,0 ,y);
    }
    
/*    void CreateWaypoints()
    {
        for (int i = 0; i < tileRoots[2].childCount; i++)
        {
            Waypoint waypoint = tileRoots[2].GetChild(i).gameObject.AddComponent<Waypoint>();
            waypoint.width = (int)_hexA;
            if (i != 0)
            {
                Waypoint previousWaypoint = tileRoots[2].GetChild(i-1).GetComponent<Waypoint>();
                previousWaypoint.nextWaypoint = waypoint;
                waypoint.previousWaypoint = previousWaypoint;
            }
        }
    }*/
}
