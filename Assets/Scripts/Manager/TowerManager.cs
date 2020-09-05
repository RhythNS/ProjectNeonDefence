using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{

    public Tower towerPref;
    // Keeping track of the current index of which tower to check
    private static int currentTowerListIndex = 0;

    // How many towers at max should be checked / updated per frame
    private static readonly int MAX_TOWERS_CHECKED_PER_STEP = 3;

    // Singleton instance
    public static TowerManager instance;
    
    // Debug Material for enemies that are in range of the tower
    public Material debugEnemyInRangeMaterial;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdateTowers()
    {
       
        
        
        // Keeping track of towers we have already checked
        var checkedTowers = new List<Tower>();
        for (var i = 0; i < MAX_TOWERS_CHECKED_PER_STEP; i++)
        {
            // If there are no towers to check, why bother?
            if (GameManager.Instance.AliveTowers.Count <= 0) break;
            // Increment to next tower index
            currentTowerListIndex++;
            // Wrap index to prevent overflow
            if (currentTowerListIndex >= GameManager.Instance.AliveTowers.Count) currentTowerListIndex = 0;
            // Get next tower and check if it has been updated this frame
            var tower = GameManager.Instance.AliveTowers[currentTowerListIndex];
            if (checkedTowers.Contains(tower)) continue;
            checkedTowers.Add(tower);

            var nearbyColliders = Physics.OverlapSphere(tower.GetCurrentPosition(), tower.effectiveRange);
            tower.enemiesInRange.Clear();
            for (var j = 0; j < nearbyColliders.Length; j++)
            {
                if (nearbyColliders[j].TryGetComponent<Enemy>(out Enemy e))
                {
                    tower.enemiesInRange.Add(e);
                    e.gameObject.GetComponent<Renderer>().material = debugEnemyInRangeMaterial;
                }
                
            }
        }
    }
}
