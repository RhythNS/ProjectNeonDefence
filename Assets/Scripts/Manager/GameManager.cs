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
    public Wave CurrentWave
    {
        get
        {
            if (CurrentLevel == null)
                return null;
            return CurrentLevel.waves.Length <= AtWave ? CurrentLevel.waves[AtWave] : null;
        }
    }

    public EnemySpawnPoint[] SpawnPoints { get; set; }

    private int AtWave = -1;

    public List<Enemy> AliveEnemies { get; private set; } = new List<Enemy>();
    
    public List<Tower> AliveTowers { get; private set; } = new List<Tower>();

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
        WaveSpawner.Instance.StartSpawning();
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
