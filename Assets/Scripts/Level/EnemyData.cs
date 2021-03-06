﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "NeonDefence/Enemy")]
public class EnemyData : ScriptableObject
{
    public int health;
    public int moneyDrop;
    public BehaviourData[] behaviours;
    public GameObject model;
    public float speed;
    public int homeDamage;
}
