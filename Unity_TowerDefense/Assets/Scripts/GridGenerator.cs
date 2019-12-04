using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public Texture2D mapTexture;

    public Color32[] colors;
    public GameObject[] hexPrefabs;
    public Transform[] tileRoots;

    private float _hexA;
    private float _hexH;
    public Vector3 _startPosition = Vector3.zero;

    private void Start()
    {
        _hexA = hexPrefabs[0].GetComponent<Renderer>().bounds.size.x / 2;
        _hexH = hexPrefabs[0].GetComponent<Renderer>().bounds.size.z / 2;

        _startPosition.x = -_hexA * 1.5f * (mapTexture.width / 2);
        _startPosition.z =_hexH * 2 * (mapTexture.width / 2);

        CreateGrid();
        /*CreateWaypoints();*/
    }

    void CreateGrid()
    {
        for (int y = 0; y < mapTexture.height; y++)
        {
            for (int x = 0; x < mapTexture.width; x++)
            {
                int index = GetTileFromImage(x,y);
                GameObject hex = Instantiate(hexPrefabs[index], tileRoots[index]);
                hex.transform.localPosition = CalcWorldPos(new Vector2Int(x, y));
                hex.name = "Hexagon X: " + x + " Y: " + y;
            }
        }
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
        float x = _startPosition.x + gridPos.x * (_hexA * 1.5f);
        float y = _startPosition.z - gridPos.y * _hexH * 2 - gridPos.x % 2 * _hexH;
        
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
