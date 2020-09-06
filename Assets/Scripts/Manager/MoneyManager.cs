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
        set
        {
            currentMoney = value;
            moneyDisplay.text = value.ToString();
        }
    }
    [SerializeField] private int currentMoney;

    public void EnemyKilled(Enemy enemy)
    {
        CurrentMoney += enemy.MoneyDrop;
    }

    public bool CanPlaceTower(Tower tower)
    {
        Debug.Log("Tower=" + tower.cost + " Current=" + currentMoney);
        if (currentMoney >= tower.cost)
        {
            currentMoney -= tower.cost;
            return true;
        }
        return false;
    }
}
