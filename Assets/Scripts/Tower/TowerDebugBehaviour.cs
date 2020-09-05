using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDebugBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Tile tile = World.Instance.Tiles.Get(3, 3);
            Tower newTower = Instantiate(TowerManager.instance.towerPref);
            var pos = tile.transform.position;
            pos.y += 3;
            newTower.transform.position = pos;

        }
    }
}