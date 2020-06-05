using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Level", menuName = "Level/New Level", order = 1)]
public class LevelSO : ScriptableObject
{
    public string levelName;
    public int level;
    public Texture2D mapTexture;
    public Color32[] colorsOnTheMap;
    
    public GameObject[] hexPrefabs;
    
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
