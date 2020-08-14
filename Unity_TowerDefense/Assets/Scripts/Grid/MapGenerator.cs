using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public Transform[] tileRoots;
    
    private LevelSO _levelData;
    private Tile[,] _grid;
    private int _mapW, _mapH;
    private float _hexW, _hexH;
    private Vector3 _startPosition = Vector3.zero;
    private RoadTile _endTile;
    
    public void Init(LevelSO data)
    {
        _levelData = data;
        _mapW = _levelData.mapTexture.width;
        _mapH = _levelData.mapTexture.height;
        
        _grid = new Tile[_mapW, _mapH];

        Vector3 size = _levelData.configs[0].hexPrefab.GetComponent<Renderer>().bounds.size;
        
        _hexW = size.x / 2;
        _hexH = size.z / 2;

        _startPosition.x = -_hexW * 1.5f * (_mapW / 2);
        _startPosition.z =_hexH * 2 * (_mapW / 2);

        CreateGrid();
        GenerateEnv();
        
        Invoke("BuildRoads", 0.1f );
        Invoke("GenerateWaves", 0.2f );
    }
    
    void CreateGrid()
    {
        for (int y = 0; y < _mapH; y++)
        {
            for (int x = 0; x < _mapW; x++)
            {
                Vector2Int gridPos = new Vector2Int(x,y);

                int index = GetTileIndexFromImage(x, y);
                Tile hex = Instantiate(_levelData.configs[index].hexPrefab, tileRoots[index]).GetComponent<Tile>();
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
        
        if (_levelData.generateTrees)
        {
            for (int i = 0; i < _levelData.treesAmount; i++)
            {
                int index = Random.Range(0, tileRoots[0].childCount);
                Tile tile = tileRoots[0].GetChild(index).GetComponent<Tile>();

                if (!tilesTrees.Contains(tile))
                {
                    tilesTrees.Add(tile);
                }
                else
                {
                    _levelData.treesAmount++;
                }
            }
            
            foreach (var tile in tilesTrees)
            {
                int index = Random.Range(0, _levelData.treesPrefabs.Length);
                Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject tree = Instantiate(_levelData.treesPrefabs[index], tile.transform.position, rotation, root);
            }
        }

        if (_levelData.generateGrass)
        {
            for (int j = 0; j < _levelData.grassAmount; j++)
            {
                int index = Random.Range(0, tileRoots[0].childCount);
                Tile tile = tileRoots[0].GetChild(index).GetComponent<Tile>();
                    
                Vector3 pos = tile.transform.position + Random.insideUnitSphere * 2;
                pos.y = root.position.y;
                Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject gr = Instantiate(_levelData.grassPrefabs[Random.Range(0, _levelData.grassPrefabs.Length)], pos, rotation, root);
            }
        }

        if (_levelData.generateProps)
        {
            for (int i = 0; i < _levelData.propsAmount; i++)
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
                int index = Random.Range(0, _levelData.propsPrefabs.Length);
                Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                GameObject building = Instantiate(_levelData.propsPrefabs[index], tile.transform.position, rotation, root);
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
        FindObjectOfType<WavesManager>().Init(_levelData);
    }
    
    private int GetTileIndexFromImage(int x, int y)
    {
        Color32 pixelColor = _levelData.mapTexture.GetPixel(x, _mapH - 1 - y);    // y offset for converting to hexagonal system

        int index = 0;
        for (int i = 0; i < _levelData.configs.Length; i++)
        {
            if (_levelData.configs[i].color.Equals(pixelColor))
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
