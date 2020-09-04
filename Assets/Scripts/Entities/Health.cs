using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IDieable))]
public class Health : MonoBehaviour
{

    [SerializeField] private IDieable diable;
    private int healthpoints;

    public int Healthpoints => healthpoints;
    public void TakeDamage(int amount)
    {
        healthpoints -= amount;
        if(amount < 0)
        {
            diable.Die();
        }
    }


}
