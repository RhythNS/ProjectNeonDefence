using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            return AtWave < CurrentLevel.waves.Length ? CurrentLevel.waves[AtWave] : null;
        }
    }

    public EnemySpawnPoint[] SpawnPoints { get; set; }

    private int AtWave = -1;

    public List<Enemy> AliveEnemies { get; private set; } = new List<Enemy>();
    // List of all towers currently alive.
    public List<Tower> AliveTowers { get; private set; } = new List<Tower>();

  
    
    [SerializeField] private Level debugLevel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadLevel(debugLevel);
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
        GetComponent<WorldGen>().Generate(level.worldGenSettings);
        EnemyPathManager.Instance.OnNextLevel();
        OnWaveEnded();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
            LoadLevel(debugLevel);
        
        //PHA: Doing tower update logic
        TowerManager.instance.UpdateTowers();
    }

    
}
