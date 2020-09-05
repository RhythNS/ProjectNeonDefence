using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : MonoBehaviour
{
    public static GameConstants Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public float TimeBetweenRounds { get => timeBetweenRounds; set => timeBetweenRounds = value; }
    [SerializeField] private float timeBetweenRounds;
    [SerializeField] private float hardTowerFValue = 10;

    public float HardTowerFValue => hardTowerFValue;

}
