﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotTower : Tower
{
    public float timeBetweenShots;
    public float currentCooldown;
    public MeeleBullet baseBullet;
    
   
    void Update()
    {
        currentCooldown += Time.deltaTime;
        if (currentCooldown >= timeBetweenShots)
            Shoot();
    }

    void Shoot()
    {
        if (targetEnemy)
        {
            currentCooldown = 0;
            MeeleBullet newBullet = Instantiate(baseBullet);
            newBullet.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            newBullet.Target = targetEnemy;
            
        }
    }
}