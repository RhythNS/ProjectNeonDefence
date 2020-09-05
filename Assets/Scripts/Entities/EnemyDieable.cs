using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieable : MonoBehaviour, IDieable
{
    public void Die()
    {
        MoneyManager.Instance.EnemyKilled(GetComponent<Enemy>());
        Destroy(gameObject);
    }

}
