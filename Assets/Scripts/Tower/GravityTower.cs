using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GravityTower : Tower
{

     public GravityTowerBullet basedBullet;

     public float currentCooldown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown += Time.deltaTime;
        if (currentCooldown >= GameConstants.Instance.TowerBulletCooldownSeconds)
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
