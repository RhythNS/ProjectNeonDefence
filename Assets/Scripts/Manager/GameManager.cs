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
        ++AtWave;
        WaveEndTimer.Instance.StartCountdown(GameConstants.Instance.TimeBetweenRounds);
    }

    public void OnBeginNextWave()
    {

    }

    public void LoadLevel(Level level)
    {
        CurrentLevel = level;
        OnWaveEnded();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
            LoadLevel(debugLevel);
    }
}
