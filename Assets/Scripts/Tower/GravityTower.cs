using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GravityTower : Tower
{

     public GravityTowerBullet basedBullet;

     public float currentCooldown;

     public float cooldownBetweenShots;

     public float slowdownPercentage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown += Time.deltaTime;
        if (currentCooldown >= cooldownBetweenShots)
        {
            currentCooldown = 0;
            ShootGravityBoolet();
        }
    }

    void ShootGravityBoolet()
    {
        if (targetEnemy != null)
        {
            var newBoolet = Instantiate(basedBullet);
            newBoolet.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            newBoolet.Target = targetEnemy;
        }
        
    }
}
