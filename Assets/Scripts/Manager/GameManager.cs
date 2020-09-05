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

    // Keeping track of the current index of which tower to check
    private int currentTowerListIndex = 0;

    // How many towers at max should be checked / updated per frame
    private readonly int MAX_TOWERS_CHECKED_PER_STEP = 3;
    
    [SerializeField] private Level debugLevel;

    private void Awake()
    {
        Instance = this;
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
        UpdateTowers();
    }

    private void UpdateTowers()
    {
        // Keeping track of towers we have already checked
        var checkedTowers = new List<Tower>();
        for (var i = 0; i < MAX_TOWERS_CHECKED_PER_STEP; i++)
        {
            // If there are no towers to check, why bother?
            if (AliveTowers.Count <= 0) break;
            // Increment to next tower index
            currentTowerListIndex++;
            // Wrap index to prevent overflow
            if (currentTowerListIndex >= AliveTowers.Count) currentTowerListIndex = 0;
            // Get next tower and check if it has been updated this frame
            var tower = AliveTowers[currentTowerListIndex];
            if (checkedTowers.Contains(tower)) continue;
            checkedTowers.Add(tower);

            var nearbyColliders = Physics.OverlapSphere(tower.GetCurrentPosition(), tower.EffectiveRange);
            tower.enemiesInRange.Clear();
            
            for (var j = 0; j < nearbyColliders.Length; j++)
            {
                if (nearbyColliders[i].TryGetComponent<Enemy>(out Enemy e))
                {
                    tower.enemiesInRange.Add(e);
                }
            }
        }
    }
}
