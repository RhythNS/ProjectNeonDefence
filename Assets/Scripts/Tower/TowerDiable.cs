using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDiable : MonoBehaviour, IDieable
{
    public void Die()
    {
        Debug.Log("oof");
        Vector2Int pos = World.Instance.WorldToGrid(transform.position);
        World.Instance.Tiles.Get(pos.x, pos.y).Tower = null; 
        GameManager.Instance.AliveTowers.Remove(GetComponent<Tower>());
        Destroy(this.gameObject);
    }
    
}
