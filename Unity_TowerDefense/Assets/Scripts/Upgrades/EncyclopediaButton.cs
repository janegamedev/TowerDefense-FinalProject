using System;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaButton: MonoBehaviour
{
    public EnemySO enemySo;

    private void Start()
    {
        Image[] images = GetComponentsInChildren<Image>();
        foreach (var image in images)
        {
            if (image.transform.parent == transform)
            {
                image.sprite = enemySo.enemyIcon;
            }
        }
    }
}