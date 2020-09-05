using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyTower : MonoBehaviour
{
    // Start is called before the first frame update
    public float moneyGrandCooldown;
    public int moneyYield;
    private float currentCooldown;
    void Start()
    {
        
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
        MoneyManager.Instance.CurrentMoney += moneyYield;
    }
}
