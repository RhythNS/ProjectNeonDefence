using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDiable : MonoBehaviour, IDieable
{
    public void Die()
    {
        Debug.Log("oof");
    }
    
}
