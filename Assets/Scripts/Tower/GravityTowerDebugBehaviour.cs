using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTowerDebugBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            Tile tile = World.Instance.Tiles.Get(1, 4);
            GravityTower newTower = Instantiate(GravityTowerManager.Instance.gravityTowerPref);
            var pos = tile.transform.position;
            pos.y += 1;
            newTower.transform.position = pos;
            tile.Tower = newTower;
            Debug.Log(tile.Tower);
        }
    }
}
