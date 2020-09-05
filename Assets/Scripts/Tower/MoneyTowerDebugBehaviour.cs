using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoneyTowerDebugBehaviour : MonoBehaviour
{
    public MoneyTower moneyTowerPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            int randX = Random.Range(2, World.Instance.Tiles.XSize-2);
            int randY = Random.Range(2, World.Instance.Tiles.YSize-2);
            Tile t = World.Instance.Tiles.Get(randX, randY);
            if (!t || t.Tower) return;
            MoneyTower newTower = Instantiate(moneyTowerPrefab);
            newTower.transform.position = t.transform.position;
            t.Tower = newTower;
        }
    }
}
