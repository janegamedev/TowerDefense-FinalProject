using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class GridGenerator : MonoBehaviour
{
    public Texture2D mapTexture;
    public Color32[] colors;
    public GameObject[] hexPrefabs;
    public Transform[] tileRoots;

    private Tile[,] _grid;

    private float _hexW;
    private float _hexH;
    private Vector3 _startPosition = Vector3.zero;

    private RoadTile _endTile;

    public bool generateTrees;
    public bool generateGrass;
    public bool generateFlowers;
    public bool generateBuildings;
    
    public int treesAmount;
    public int grassAmountPerTile;
    public int flowersAmountPerTile;
    [FormerlySerializedAs("buldingsAmount")] public int buildingsAmount;
    
    public GameObject[] trees;
    public GameObject[] grass;
    public GameObject[] flowers;
    public GameObject[] buildings;
    
    public Transform envRoot;


    public void Start()
    {
        _grid = new Tile[mapTexture.width, mapTexture.height];
        
        _hexW = hexPrefabs[0].GetComponent<Renderer>().bounds.size.x / 2;
        _hexH = hexPrefabs[0].GetComponent<Renderer>().bounds.size.z / 2;

        _startPosition.x = -_hexW * 1.5f * (mapTexture.width / 2);
        _startPosition.z =_hexH * 2 * (mapTexture.width / 2);

        CreateGrid();
        GenerateEnv();
        
        _endTile = tileRoots[2].GetComponentsInChildren<RoadTile>().First(x => x.isEnd == true);
        
        Invoke("Overlap", 0.1f );
    }

    private void Overlap()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
        _endTile.PropagateRoad(null);
    }

    private void GenerateEnv()
    { 
        List<Tile> tilesTrees = new List<Tile>();
        List<Tile> tilesBuildings = new List<Tile>();
        
        if (generateTrees)
        {
            
            for (int i = 0; i < treesAmount; i++)
            {
                int index = Random.Range(0, tileRoots[0].childCount);
                Tile tile = tileRoots[0].GetChild(index).GetComponent<Tile>();

                if (!tilesTrees.Contains(tile))
                {
                    tilesTrees.Add(tile);
                }
                else
                {
                    treesAmount++;
                }
            }
            
            foreach (var tile in tilesTrees)
            {
                int index = Random.Range(0, trees.Length);
                GameObject tree = Instantiate(trees[index], tile.transform.position, Quaternion.identity, envRoot);
            }
        }

        if (generateGrass)
        {
            for (int i = 0; i < Random.Range(5,15); i++)
            {
                for (int j = 0; j < grassAmountPerTile; j++)
                {
                    int index = Random.Range(0, tileRoots[0].childCount);
                    Tile tile = tileRoots[0].GetChild(index).GetComponent<Tile>();
                    
                    Vector3 pos = tile.transform.position + Random.insideUnitSphere * 10;
                    pos.y = envRoot.transform.position.y;
                    Quaternion rot = Quaternion.Euler(0, Random.Range(0, 360), 0);
                    GameObject gr = Instantiate(grass[Random.Range(0, grass.Length)], pos, rot, envRoot);
                }
            }
        }

        if (generateFlowers)
        {
            for (int i = 0; i < Random.Range(5,15); i++)
            {
                for (int j = 0; j < flowersAmountPerTile; j++)
                {
                    int index = Random.Range(0, tileRoots[0].childCount);
                    Tile tile = tileRoots[0].GetChild(index).GetComponent<Tile>();
            
                    int flower = Random.Range(0, flowers.Length);
                    Vector3 pos = tile.transform.position + Random.insideUnitSphere * 10;
                    pos.y = envRoot.transform.position.y;
                    Quaternion rot = Quaternion.Euler(0, Random.Range(0, 360), 0);
                    GameObject f = Instantiate(flowers[flower], pos, rot, envRoot);
                }
            }
        }

        if (generateBuildings)
        {
            for (int i = 0; i < buildingsAmount; i++)
            {
                int index = Random.Range(0, tileRoots[0].childCount);
                Tile tile = tileRoots[0].GetChild(index).GetComponent<Tile>();

                if (!tilesTrees.Contains(tile) && !tilesBuildings.Contains(tile))
                {
                    tilesBuildings.Add(tile);
                }
                else
                {
                    buildingsAmount++;
                }
            }
            
            foreach (var tile in tilesBuildings)
            {
                int index = Random.Range(0, buildings.Length);
                Quaternion rot = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject building = Instantiate(buildings[index], tile.transform.position, rot, envRoot);
            }
        }

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
                _grid[x,y] = hex;
                hex.name = "Hexagon X: " + x + " Y: " + y;
            }
        }
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
}
