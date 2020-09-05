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
            int randX = Random.Range(2, World.Instance.Tiles.XSize - 2);
            int randY = Random.Range(2, World.Instance.Tiles.YSize - 2);
            if (GameManager.Instance.CurrentLevel.worldGenSettings.homePosition ==
                new Vector2Int(randX, randY)) return;
            Tile t = World.Instance.Tiles.Get(randX, randY);
            if (!t || t.Tower) return;
            GravityTower newTower = Instantiate(GravityTowerManager.Instance.gravityTowerPref);
            newTower.transform.position = t.transform.position;
            t.Tower = newTower;
        }
    }
}