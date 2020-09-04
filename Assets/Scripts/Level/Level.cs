using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : ScriptableObject
{
    [SerializeField] private Vector2Int basePoint;
    [SerializeField] private Vector2Int[] spawnPoints;
}
