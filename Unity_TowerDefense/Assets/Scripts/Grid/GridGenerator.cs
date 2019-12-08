using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class GridGenerator : MonoBehaviour
{
    public LevelSO levelSo;
    public Transform[] tileRoots;
    
    private Texture2D mapTexture;
    private Color32[] colors;
    private GameObject[] hexPrefabs;

    private Tile[,] _grid;

    private float _hexW;
    private float _hexH;
    private Vector3 _startPosition = Vector3.zero;

    private RoadTile _endTile;

    private bool _generateTrees = false;
    private GameObject[] _treesPrefabs;
    private int _treesAmount;
    
    private bool _generateGrass = false;
    private GameObject[] _grassPrefabs;
    private int _grassAmount;

    private bool _generateProps = false;
    private GameObject[] _propsPrefabs;
    private int _propsAmount;

    public void Init(LevelSO data)
    {
        levelSo = data;
        mapTexture = data.mapTexture;
        colors = data.colorsOnTheMap;
        hexPrefabs = data.hexPrefabs;
        _generateTrees = data.generateTrees;

        if (_generateTrees)
        {
            _treesPrefabs = data.treesPrefabs;
            _treesAmount = data.treesAmount;
        }
        
        _generateGrass = data.generateGrass;

        if (_generateGrass)
        {
            _grassPrefabs = data.grassPrefabs;
            _grassAmount = data.grassAmount;
        }
        
        _generateProps = data.generateProps;

        if (_generateProps)
        {
            _propsPrefabs = data.propsPrefabs;
            _propsAmount = data.propsAmount;
        }
        
        _grid = new Tile[mapTexture.width, mapTexture.height];
        
        _hexW = hexPrefabs[0].GetComponent<Renderer>().bounds.size.x / 2;
        _hexH = hexPrefabs[0].GetComponent<Renderer>().bounds.size.z / 2;

        _startPosition.x = -_hexW * 1.5f * (mapTexture.width / 2);
        _startPosition.z =_hexH * 2 * (mapTexture.width / 2);

        CreateGrid();
        GenerateEnv();
        
        Invoke("BuildRoads", 0.1f );
        Invoke("GenerateWaves", 0.2f );
        
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
    
    private void GenerateEnv()
    {
        Transform root = tileRoots[tileRoots.Length - 1];
        List<Tile> tilesTrees = new List<Tile>();
        List<Tile> tilesProps = new List<Tile>();
        
        if (_generateTrees)
        {
            
            for (int i = 0; i < _treesAmount; i++)
            {
                int index = Random.Range(0, tileRoots[0].childCount);
                Tile tile = tileRoots[0].GetChild(index).GetComponent<Tile>();

                if (!tilesTrees.Contains(tile))
                {
                    tilesTrees.Add(tile);
                }
                else
                {
                    _treesAmount++;
                }
            }
            
            foreach (var tile in tilesTrees)
            {
                int index = Random.Range(0, _treesPrefabs.Length);
                Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject tree = Instantiate(_treesPrefabs[index], tile.transform.position, rotation, root);
            }
        }

        if (_generateGrass)
        {
            for (int j = 0; j < _grassAmount; j++)
            {
                int index = Random.Range(0, tileRoots[0].childCount);
                Tile tile = tileRoots[0].GetChild(index).GetComponent<Tile>();
                    
                Vector3 pos = tile.transform.position + Random.insideUnitSphere * 10;
                pos.y = root.position.y;
                Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject gr = Instantiate(_grassPrefabs[Random.Range(0, _grassPrefabs.Length)], pos, rotation, root);
            }
        }

        if (_generateProps)
        {
            for (int i = 0; i < _propsAmount; i++)
            {
                int index = Random.Range(0, tileRoots[0].childCount);
                Tile tile = tileRoots[0].GetChild(index).GetComponent<Tile>();

                if (!tilesTrees.Contains(tile) && !tilesProps.Contains(tile))
                {
                    tilesProps.Add(tile);
                }
            }
            
            foreach (var tile in tilesProps)
            {
                int index = Random.Range(0, _propsPrefabs.Length);
                Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject building = Instantiate(_propsPrefabs[index], tile.transform.position, rotation, root);
            }
        }

    }

    private void BuildRoads()
    {
        _endTile = tileRoots[1].GetComponentsInChildren<RoadTile>().First(x => x.isEnd == true);
        GetComponent<NavMeshSurface>().BuildNavMesh();
        _endTile.PropagateRoad(null);
    }

    private void GenerateWaves()
    {
        FindObjectOfType<WavesManager>().Init(levelSo);
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
