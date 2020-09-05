using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDict : MonoBehaviour
{
    public static TowerDict Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Tower[] towerPrefabs;

    public Tower[] TowerPrefabs => towerPrefabs;
}
