using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/New Enemy", order = 1)]
public class EnemySO : ScriptableObject
{
   public string enemyName;
   public Sprite enemyIcon;
   public string enemyDescription;
   public GameObject enemyModel;
   public float health;
   public float armour;
   public float magicResistance;
   public float speed;
   public int bounty;

}
