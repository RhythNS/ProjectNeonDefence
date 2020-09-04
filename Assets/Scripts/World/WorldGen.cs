using MonoNet.Util.Datatypes;
using System.Linq;
using System.Xml.Schema;
using UnityEngine;

public class WorldGen : MonoBehaviour
{
    [SerializeField] private GameObject[] buildableTiles;
    [SerializeField] private GameObject[] unbuildableTiles;
    [SerializeField] private GameObject[] borderTiles;
    [SerializeField] private GameObject spawnTiles;
    [SerializeField] private GameObject homeTile;

    [SerializeField] private Vector2 tileSize;

    [SerializeField] private float unblockablePercentange;
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;

    [SerializeField] private Vector2Int homePosition;
    [SerializeField] private Vector2Int[] spawnPoints;
    [SerializeField] private Vector3 offset;

    public void Generate()
    {
        Fast2DArray<Tile> tiles = new Fast2DArray<Tile>(sizeX + 1, sizeY + 1);
        world = new GameObject("World").AddComponent<World>();

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                GameObject gameObject;
                bool isBuildable = false;

                bool isSpawnPoint = false;
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    if (spawnPoints[i].x == x && spawnPoints[i].y == y)
                    {
                        isSpawnPoint = true;
                        break;
                    }
                }

                if (isSpawnPoint == true)
                    gameObject = Instantiate(spawnTiles, world.transform);
                else if (x == 0 || x == sizeX - 1 || y == 0 || y == sizeY - 1)
                    gameObject = Instantiate(ArrayUtil<GameObject>.RandomElement(borderTiles), world.transform);
                else if (homePosition.x == x && homePosition.y == y)
                    gameObject = Instantiate(homeTile, world.transform);
                else if (isBuildable = Random.value < unblockablePercentange)
                    gameObject = Instantiate(ArrayUtil<GameObject>.RandomElement(unbuildableTiles), world.transform);
                else
                    gameObject = Instantiate(ArrayUtil<GameObject>.RandomElement(buildableTiles), world.transform);

                Tile tile = gameObject.AddComponent<Tile>();
                tile.Set(isBuildable, x, y);

                Vector3 pos = offset;
                pos.x += x * tileSize.x;
                pos.z += y * tileSize.y;

                gameObject.transform.position = pos;

                // TODO: PLEASE REMOVE THIS AFTER REPLACING THE MODELS:
                gameObject.transform.localScale = new Vector3(tileSize.x, 1, tileSize.y);
            }
        }

        world.Set(tiles, tileSize);
    }

    // TODO: PLEASE REMOVE THIS :
    private World world;
    private float value1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) == true)
        {
            if (world != null)
                Destroy(world.gameObject);
            Generate();
        }
    }
}
