using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        InWave
    }

    public static GameManager Instance { get; private set; }

    public Level CurrentLevel { get; private set; }
    private int AtWave = -1;

    [SerializeField] private Level debugLevel;

    private void Awake()
    {
        Instance = this;
    }

    public void OnWaveEnded()
    {

    }

    public void OnSkipWait()
    {

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
