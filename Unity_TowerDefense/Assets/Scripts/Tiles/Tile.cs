using UnityEngine;

public class Tile : MonoBehaviour
{
    private Vector2Int gridPosition;
    public Vector2Int GridPosition => gridPosition;

    public void SetGridPosition(Vector2Int pos)
    {
        gridPosition = pos;
    }
}