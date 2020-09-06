using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    [SerializeField] private TMP_Text moneyDisplay;

    private void Awake()
    {
        Instance = this;
    }

    public int CurrentMoney
    {
        get => currentMoney;
        private set
        {
            currentMoney = value;
            moneyDisplay.text = value.ToString();
        }
    }
    [SerializeField] private int currentMoney;

    public void EnemyKilled(Enemy enemy)
    {
        //currentMoney += enemy.MoneyDrop;
        ModifyMoney(enemy.MoneyDrop);
    }

    public bool CanPlaceTower(Tower tower)
    {
        Debug.Log("Tower="+tower.cost+" Current="+currentMoney);
        return currentMoney >= tower.cost;
    }

    public void ModifyMoney(int amount)
    {
        this.CurrentMoney += amount;
    }

}
