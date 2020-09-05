using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDict : MonoBehaviour
{
    public static MaterialDict Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Material[] materialPrefabs;

    public Material[] MaterialPrefabs => materialPrefabs;
}
