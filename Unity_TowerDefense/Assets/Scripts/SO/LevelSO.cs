using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level/New Level", order = 1)]
public class LevelSO : ScriptableObject
{
    public int level;
    public Texture2D mapTexture;
    
    public TileColorConfig[] configs;
    
    public bool generateTrees;
    public GameObject[] treesPrefabs;
    public int treesAmount;
    
    public bool generateGrass;
    public GameObject[] grassPrefabs;
    public int grassAmount;

    public bool generateProps;
    public GameObject[] propsPrefabs;
    public int propsAmount;

    public Wave[] waves;
    
    public LevelSO nextLevel;
}

[System.Serializable]
public class TileColorConfig
{
    public Color32 color;
    public GameObject hexPrefab;
}
