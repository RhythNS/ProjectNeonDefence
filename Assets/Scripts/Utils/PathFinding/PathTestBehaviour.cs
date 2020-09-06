using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        if (Input.GetKeyDown(KeyCode.F12))
        {
            World.Instance.Tiles.Get(start.x, start.y).GetComponent<Renderer>().material = pathColor;
            World.Instance.Tiles.Get(target.x, target.y).GetComponent<Renderer>().material = pathColor;
            var tiles = new SimpleAStar().GeneratePath(World.Instance.Tiles.Get(start.x, start.y),
                World.Instance.Tiles.Get(target.x, target.y));

            if (tiles == null)
            {
                Debug.Log("Tiles are null!");
                return;
            }

            StartCoroutine(StartDrawing(tiles));
        }
    }


    private IEnumerator StartDrawing(List<Tile> tiles)
    {
        foreach (var tile in tiles)
        {
            tile.GetComponent<Renderer>().material = pathColor;
            Debug.Log("Tower: " + tile.Tower);
            yield return new WaitForSeconds(0.1f);
        }
    }
}