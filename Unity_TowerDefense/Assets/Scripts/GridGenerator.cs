using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public Texture2D mapTexture;
    public GameObject tilePrefab;
    
    
    [SerializeField] private Color grassColor;
    [SerializeField] private Color roadColor;
    [SerializeField] private Color waterColor;
    [SerializeField] private Color towerPlaceColor;
    
    [SerializeField] private Vector2Int mapSize;
    [SerializeField] private float tileRadius;
    
    [SerializeField] private float _tileDiameter;
    
    private void Start()
    {
        Debug.Log(grassColor);
        _tileDiameter = tileRadius * 2;
        mapSize = new Vector2Int(mapTexture.height, mapTexture.width);
        Debug.Log(mapSize);
        
        GenerateTexture();
    }
    
    private void GenerateTexture()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 worldPoint = GetWorldPosition(new Vector2Int(x, y));
                Color tileColor = mapTexture.GetPixel(x,y);
                GameObject newTile = Instantiate(tilePrefab, worldPoint, Quaternion.Euler(Vector3.right));
                newTile.GetComponent<Renderer>().material.color = tileColor;
                Debug.Log(tileColor);
                if (tileColor == grassColor)
                {
                    newTile.name = "Grass";
                    Debug.Log("Grass");
                }
                else if (tileColor == roadColor)
                {
                    newTile.name = "Road";
                }
                else if (tileColor == waterColor)
                {
                    newTile.name = "Water";
                }
                else if (tileColor == towerPlaceColor)
                {
                    newTile.name = "TowerPlace";
                }
            }
        }
    }
    
    private Vector3 GetWorldPosition(Vector2Int tilePosition)
    {
        return Vector3.zero + new Vector3((tilePosition.x - mapSize.x / 2) * _tileDiameter + tileRadius, 0,
                   (tilePosition.y - mapSize.y / 2) * _tileDiameter + tileRadius);
    }
    
    
}
