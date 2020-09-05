using System.Collections;
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
            newBullet.transform.position = transform.position;
            newBullet.Target = targetEnemy;
        }
    }


    // -- UPGRADES --\\
    public int Rank => rank;
    private int rank = 1;
    [SerializeField] private UpgradePath[] upgradePaths;

    public UpgradePath NextUpgradePath
    {
        get => nextUpgradePath;
        set => nextUpgradePath = value;
    }

    private UpgradePath nextUpgradePath;

    public bool Upgrade()
    {
        if (MoneyManager.Instance.CurrentMoney < nextUpgradePath.Cost)
        {
            return false;
        }

        this.timeBetweenShots = nextUpgradePath.TimeBetweenShots;
        if (rank < upgradePaths.Length)
            nextUpgradePath = upgradePaths[rank - 1];
        rank++;
        return true;
    }

    public class UpgradePath
    {
        int cost;
        float timeBetweenShots;

        public int Cost
        {
            get => cost;
        }


        public float TimeBetweenShots => timeBetweenShots;
    }
}