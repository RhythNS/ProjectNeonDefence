using MonoNet.Util.Datatypes;
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
                    gameObject = Instantiate(ArrayUtil<GameObject>.RandomElement(borderTiles), world.transform);
                else if (settings.homePosition.x == x && settings.homePosition.y == y)
                    gameObject = Instantiate(homeTile, world.transform);
                else if (isBuildable = Random.value < settings.unblockablePercentange)
                    gameObject = Instantiate(ArrayUtil<GameObject>.RandomElement(unbuildableTiles), world.transform);
                else
                    gameObject = Instantiate(ArrayUtil<GameObject>.RandomElement(buildableTiles), world.transform);

                Tile tile = gameObject.AddComponent<Tile>();
                tile.Set(isBuildable, x, y);

                Vector3 pos = offset;
                pos.x += x * tileSize.x;
                pos.z += y * tileSize.y;

                gameObject.transform.position = pos;

                // TODO: PLEASE REMOVE THIS AFTER REPLACING THE MODELS
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
            Generate(debugSettings);
        }
    }
}
