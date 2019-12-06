using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public LevelSO currentLevel;
    
    private void Start()
    {
        GridGenerator gg = FindObjectOfType<GridGenerator>();
        gg.Init(currentLevel);
    }
}
