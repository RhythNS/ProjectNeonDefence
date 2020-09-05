using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotTowerDebugBehaviour : MonoBehaviour
{
    public SingleShotTower towerPrefab;
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            int randX = Random.Range(2, World.Instance.Tiles.XSize-2);
            int randY = Random.Range(2, World.Instance.Tiles.YSize-2);
            Tile t = World.Instance.Tiles.Get(randX, randY);
            if (!t || t.Tower) return;
            SingleShotTower newTower = Instantiate(towerPrefab);
            newTower.transform.position = t.transform.position;
            t.Tower = newTower;
        }
    }
}
