using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotTower : Tower
{
    public float timeBetweenShots;
    public float currentCooldown;
    public MeeleBullet baseBullet;
    private int currenAimPoint = 0;

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
            this.transform.LookAt(targetEnemy.transform.position);
            currentCooldown = 0;
            MeeleBullet newBullet = Instantiate(baseBullet);
            newBullet.transform.position = Aimpoint[currenAimPoint].position;
            newBullet.Target = targetEnemy;
            ++currenAimPoint;
            currenAimPoint %= Aimpoint.Length;
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

    public override bool Upgrade()
    {
        if (nextUpgradePath == null) nextUpgradePath = upgradePaths[0];
        
        if (MoneyManager.Instance.CurrentMoney < nextUpgradePath.Cost)
        {
            return false;
        }
        if (rank > upgradePaths.Length) return false;
        
        MoneyManager.Instance.CurrentMoney -= nextUpgradePath.cost;
        CurrentValue += nextUpgradePath.cost;


        this.timeBetweenShots = nextUpgradePath.TimeBetweenShots;
        if (rank < upgradePaths.Length)
            nextUpgradePath = upgradePaths[rank];
        rank++;
        return true;
    }
    [System.Serializable]
    public class UpgradePath
    {
        public int cost;
        public float timeBetweenShots;

        public int Cost
        {
            get => cost;
        }


        public float TimeBetweenShots => timeBetweenShots;
    }
}