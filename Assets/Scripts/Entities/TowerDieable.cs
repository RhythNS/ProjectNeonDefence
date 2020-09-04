using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDieable : MonoBehaviour, IDieable
{
    public void Die()
    {
        Debug.Log("The tower " + this.gameObject.name + " got destroyed!");
        Destroy(this.gameObject);
        //TODO Fancy effekte
    }
}
