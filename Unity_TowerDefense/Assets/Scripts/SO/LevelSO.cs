using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level/New Level", order = 1)]
public class LevelSO : ScriptableObject
{
    public string levelName;
    public Sprite levelMap;

    public LevelSO nextLevel;
    
}
