using MonoNet.Util.Datatypes;
using System.Diagnostics;
using UnityEngine;

public class WorldGen : MonoBehaviour
{
    [SerializeField] private GameObject[] buildableTiles;
    [SerializeField] private GameObject[] unbuildableTiles;
    [SerializeField] private GameObject[] borderTiles;
    [SerializeField] private GameObject spawnTiles;
    [SerializeField] private GameObject homeTile;

    [SerializeField] private Vector2 tileSize;

    [SerializeField] private Vector3 offset;

    [SerializeField] private WorldGenSettings debugSettings;

    public void Generate(WorldGenSettings settings)
    {
        Fast2DArray<Tile> tiles = new Fast2DArray<Tile>(settings.sizeX + 1, settings.sizeY + 1);
        world = new GameObject("World").AddComponent<World>();

        for (int x = 0; x < settings.sizeX; x++)
        {
            for (int y = 0; y < settings.sizeY; y++)
            {
                GameObject gameObject;
                bool isBuildable = false;

                bool isSpawnPoint = false;
                for (int i = 0; i < settings.spawnPoints.Length; i++)
                {
                    if (settings.spawnPoints[i].x == x && settings.spawnPoints[i].y == y)
                    {
                        isSpawnPoint = true;
                        break;
                    }
                }

                if (isSpawnPoint == true)
                    gameObject = Instantiate(spawnTiles, world.transform);
                else if (x == 0 || x == settings.sizeX - 1 || y == 0 || y == settings.sizeY - 1)
                {
                    gameObject = Instantiate(ArrayUtil<GameObject>.RandomElement(borderTiles), world.transform);
                }
                else if (settings.homePosition.x == x && settings.homePosition.y == y)
                {
                    gameObject = Instantiate(homeTile, world.transform);
                    gameObject.AddComponent<Home>();
                }
                else if (isBuildable = Random.value < settings.unblockablePercentange)
                {
                    gameObject = Instantiate(ArrayUtil<GameObject>.RandomElement(unbuildableTiles), world.transform);
                }
                else
                {
                    gameObject = Instantiate(ArrayUtil<GameObject>.RandomElement(buildableTiles), world.transform);
                }

                Tile tile = gameObject.AddComponent<Tile>();
                tile.Set(isBuildable, x, y);

                Vector3 pos = offset;
                pos.x += x * tileSize.x;
                pos.z += y * tileSize.y;

                tiles.Set(tile, x, y);

                gameObject.transform.position = pos;

            }
        }

        EnemySpawnPoint[] spawnPoints = new EnemySpawnPoint[settings.spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = tiles.Get(settings.spawnPoints[i].x, settings.spawnPoints[i].y)
                .gameObject.AddComponent<EnemySpawnPoint>();
            spawnPoints[i].Number = i;
        }
        GameManager.Instance.SpawnPoints = spawnPoints;

        world.Set(tiles, tileSize);
        CameraController.Instance.LevelRect = new Rect(offset, new Vector2(tileSize.x * settings.sizeX, tileSize.y * settings.sizeY));
    }

    // TODO: PLEASE REMOVE THIS :
    private World world;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) == true)
        {
            if (world != null)
                Destroy(world.gameObject);
            Generate(debugSettings);
        }
    }
}
