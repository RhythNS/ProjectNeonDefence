using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDieable : MonoBehaviour, IDieable
{
    public void Die()
    {
        Debug.Log("The tower " + this.gameObject.name + " got destroyed!");
        //TODO Fancy effekte + Map needs to get updated

        //Calculate new paths for the enemies
        EnemyPathManager.Instance.OnWorldChange();

        Destroy(this.gameObject);
    }
}
