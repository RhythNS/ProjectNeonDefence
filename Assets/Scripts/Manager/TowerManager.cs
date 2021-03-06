﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{

    public Tower towerPref;
    // Keeping track of the current index of which tower to check
    private static int currentTowerListIndex = 0;
    

    // Singleton instance
    public static TowerManager Instance;
    
    // Debug Material for enemies that are in range of the tower
    public Material debugEnemyInRangeMaterial;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTowers();
    }
    
    public void UpdateTowers()
    {
       
        
        
        // Keeping track of towers we have already checked
        var checkedTowers = new List<Tower>();
        for (var i = 0; i < GameConstants.Instance.TowerManagerMaxTowersCheckedPerFrame; i++)
        {
            // If there are no towers to check, why bother?
            if (GameManager.Instance.AliveTowers.Count <= 0) break;
            // Increment to next tower index
            currentTowerListIndex++;
            // Wrap index to prevent overflow
            if (currentTowerListIndex >= GameManager.Instance.AliveTowers.Count) currentTowerListIndex = 0;
            // Get next tower and check if it has been updated this frame
            var currentTower = GameManager.Instance.AliveTowers[currentTowerListIndex];
            if (checkedTowers.Contains(currentTower)) continue;
            checkedTowers.Add(currentTower);

            // Getting nearby colliders
            var nearbyColliders = Physics.OverlapSphere(currentTower.transform.position, currentTower.Range);
            currentTower.enemiesInRange.Clear();
            for (var j = 0; j < nearbyColliders.Length; j++)
            {
                // If the nearby collider is an enemy
                if (nearbyColliders[j].TryGetComponent<Enemy>(out Enemy e))
                {
                    // Add the enemies in the range to the list for the tower
                    currentTower.enemiesInRange.Add(e);
                }
                
            }
            // Check if the tower can lock onto a new target.
            currentTower.TryUpdateEnemy(out Enemy newEnemy);
        }
    }

    public List<Vector2Int> GetLocationsOfTowers()
    {
        /*var list = new List<Vector2Int>();
        for (int i = 0; i < GameManager.Instance.AliveTowers.Count; i++)
        {
            var tower = GameManager.Instance.AliveTowers[i];
            list.Add(new Vector2Int((int) tower.transform.position.x,(int) tower.transform.position.y));
        }

        return list;*/
        return new List<Vector2Int>();
    }
}
