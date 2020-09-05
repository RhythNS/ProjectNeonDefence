using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTestBehaviour : MonoBehaviour
{
    public Material pathColor;
    public Vector2Int start;
    public Vector2Int target;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            World.Instance.Tiles.Get(start.x,start.y).GetComponent<Renderer>().material = pathColor;
            World.Instance.Tiles.Get(target.x,target.y).GetComponent<Renderer>().material = pathColor;
            var tiles = SimpleAStar.GeneratePath(World.Instance.Tiles.Get(start.x, start.y),
                World.Instance.Tiles.Get(target.x, target.y));

            if (tiles == null)
            {
                Debug.Log("Tiles are null!");
                return;
            }
            Debug.Log("Tiles " + tiles);
            foreach (var tile in tiles)
            {
                tile.GetComponent<Renderer>().material = pathColor;
            }
        }
    }
}