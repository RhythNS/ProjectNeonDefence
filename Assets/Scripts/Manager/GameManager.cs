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

    public List<Enemy> AliveEnemies {
        get;
        private set; } = new List<Enemy>();
    // List of all towers currently alive.
    public List<Tower> AliveTowers { get; private set; } = new List<Tower>();

    public List<Drone> AliveDrones { get; private set; } = new List<Drone>();

    public int RemainingHealth
    {
        get => remainingHealth;
        set
        {
            remainingHealth = value;
            if (remainingHealth <= 0)
            {
                OnGameLoose();
            }
        }
    }
    private int remainingHealth = 100;

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

    public void OnGameLoose()
    {
        Debug.Log("You lost the game");
    }

    public void LoadLevel(Level level)
    {
        CurrentLevel = level;
        MoneyManager.Instance.CurrentMoney = CurrentLevel.startingMoney;
        GetComponent<WorldGen>().Generate(level.worldGenSettings);
        EnemyPathManager.Instance.OnNextLevel();
        OnWaveEnded();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
            LoadLevel(debugLevel);

    }


}
