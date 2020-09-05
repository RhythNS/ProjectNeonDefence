using System.Collections.Generic;
using UnityEngine;

public class DroneTower : Tower
{
    public int maxDrones;
    public float timeBetweenShots;
    public float currentCooldown;
    
    public Drone dronePrefab;
    public float droneSpeed;
    public int droneHealth;
    public int droneDamage;
    public float droneDamagePerSeconds;

    public List<Drone> ActiveDrones { get; private set; } = new List<Drone>();

    void Update()
    {
        currentCooldown += Time.deltaTime;
        if (currentCooldown >= timeBetweenShots && ActiveDrones.Count < maxDrones)
            Shoot();
    }

    protected override Enemy GetNewTarget()
    {
        if (enemiesInRange == null || enemiesInRange.Count == 0)
            return null;

        Enemy nearestEnemy = enemiesInRange[0];
        float minDistance = Vector3.SqrMagnitude(enemiesInRange[0].transform.position - transform.position);
        float currDistance;
        for (int i = 1; i < enemiesInRange.Count; i++)
        {
            currDistance = Vector3.SqrMagnitude(enemiesInRange[i].transform.position - transform.position);
            if (minDistance > currDistance)
            {
                minDistance = currDistance;
                nearestEnemy = enemiesInRange[i];
            }
        }

        return nearestEnemy;
    }

    void Shoot()
    {
        if (targetEnemy)
        {
            currentCooldown = 0;
            Drone drone = Instantiate(dronePrefab, transform.position, Quaternion.identity);
            drone.Set(this, targetEnemy, droneSpeed, droneHealth, droneDamage, droneDamagePerSeconds);
            drone.targetEnemy = targetEnemy;
        }
    }

}
