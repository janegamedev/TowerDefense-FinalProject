using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum LevelState
{
    LOCKED,
    UNLOCKED,
    FINISHED
}

public class Level : MonoBehaviour
{
    public LevelState levelState;
    public int score;

    public LevelSO levelData;
    
    [SerializeField] public GameObject lockedObject;
    [SerializeField] public GameObject unlockedObject;
    [SerializeField] public GameObject finishedObject;
    [SerializeField] public GameObject[] starObjects;

    public void UpdateState(LevelState nextState)
    {
        levelState = nextState;

        switch (levelState)
        {
            case LevelState.LOCKED:
                lockedObject.SetActive(true);
                unlockedObject.SetActive(false);
                finishedObject.SetActive(false);
                
                foreach (var star in starObjects)
                {
                    star.SetActive(false);
                }
                break;
            
            case LevelState.UNLOCKED:
                lockedObject.SetActive(false);
                unlockedObject.SetActive(true);
                finishedObject.SetActive(false);

                foreach (var star in starObjects)
                {
                    star.SetActive(false);
                }
                break;
            
            case LevelState.FINISHED:
                lockedObject.SetActive(false);
                unlockedObject.SetActive(false);
                finishedObject.SetActive(true);

                int s = score;
                for (int i = 0; i < starObjects.Length; i++)
                {
                    if (i < s)
                    {
                        starObjects[i].SetActive(true);
                    }
                    else
                    {
                        starObjects[i].SetActive(false);
                    }
                }
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
