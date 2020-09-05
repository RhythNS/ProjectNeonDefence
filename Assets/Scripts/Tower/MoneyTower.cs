using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyTower : Tower
{
    // Start is called before the first frame update
    public float moneyGrandCooldown;
    public int moneyYield;
    private float currentCooldown;
    
    
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

        this.moneyYield = nextUpgradePath.MoneyYield;
        this.moneyGrandCooldown = nextUpgradePath.MoneyGrandCooldown;
        if (rank < upgradePaths.Length)
            nextUpgradePath = upgradePaths[rank - 1];
        rank++;
        return true;
    }

    public class UpgradePath
    {
        int cost;
        private int moneyYield;
        float moneyGrandCooldown;

        public int Cost
        {
            get => cost;
        }

        public int MoneyYield => moneyYield;

        public float MoneyGrandCooldown => moneyGrandCooldown;
    }
    

    // Update is called once per frame
    void Update()
    {
        // Update Cooldown
        currentCooldown += Time.deltaTime;
        if (currentCooldown > moneyGrandCooldown) GrandMoney();
    }

    void GrandMoney()
    {
        currentCooldown = 0f;
        MoneyManager.Instance.ModifyMoney(moneyYield);
    }
    
    
}