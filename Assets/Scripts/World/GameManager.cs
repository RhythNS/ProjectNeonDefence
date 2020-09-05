using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Level debugLevel;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadLevel(Level level)
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
            LoadLevel(debugLevel);
    }
}
