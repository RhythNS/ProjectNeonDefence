using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathManager : MonoBehaviour
{
    public static EnemyPathManager Instance { get; private set; }

    public Vector2Int[] SpawnPoints;

    public List<Tile>[] CurrentPaths;

    private Tile homePoint;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnNextLevel();
        //TODO Bei Eventsregistrieren (Towerplaced, TowerDieable)
        //TODO Spawnpointupdate bei neuem Level: Registrieren Beim NextLevel Event
    }

    public List<Tile> GetStartPath(int atSpawnPoint) => CurrentPaths[atSpawnPoint];

    /*
     * Eventmethod for the start of a new level
     */
    public void OnNextLevel()
    {
        SpawnPoints = GameManager.Instance.CurrentLevel.worldGenSettings.spawnPoints;
        CurrentPaths = new List<Tile>[CurrentPaths.Length];
        Vector2Int homeP = GameManager.Instance.CurrentLevel.worldGenSettings.homePosition;
        homePoint = World.Instance.Tiles.Get(homeP.x, homeP.y);

        CalculatePaths();
    }

    /*
     * Eventmethod for a worldChange
     */
    public void OnWorldChange()
    {
        CalculatePaths();
    }

    private void CalculatePaths()
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            Vector2Int spawnP = SpawnPoints[i];
            CurrentPaths[i] = SimpleAStar.GeneratePath(World.Instance.Tiles.Get(spawnP.x, spawnP.y), homePoint);
        }
    }
}
