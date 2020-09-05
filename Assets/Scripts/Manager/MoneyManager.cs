using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public int CurrentMoney { get; private set; }

    public void EnemyKilled(Enemy enemy)
    {
        //TODO: the thing
    }

    public bool CanPlaceTower(Tower tower)
    {
        // TODO: the thing
        return true;
    }
}
