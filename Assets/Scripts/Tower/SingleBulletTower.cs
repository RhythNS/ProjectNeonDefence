using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBulletTower : Tower
{
    private bool canShoot;
    private long timeSinceLastShot = 0L;
    void Update()
    {
        
    }

    protected override List<Enemy> GetNewTarget()
    {
        var resultList = new List<Enemy>();
        if (enemiesInRange.Count != 0)
            resultList.Add(enemiesInRange[0]);
        return resultList;
    }
}