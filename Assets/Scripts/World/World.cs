using MonoNet.Util.Datatypes;
using UnityEngine;

public class World : MonoBehaviour
{
    public static World Instance { get; private set; }

    public Fast2DArray<Tile> Tiles { get; private set; }

    [SerializeField] private Vector2 tileSize;
    public Vector2 TileSize => tileSize;

    public Vector2Int WorldToGrid(Vector3 worldPos) => new Vector2Int((int)(worldPos.x / tileSize.x), (int)(worldPos.y / tileSize.y));
    public Vector3 GridToWorld(Vector2Int worldPos) => new Vector3(worldPos.x * tileSize.x, transform.position.y, worldPos.y * tileSize.y);
    //TODO: Change when known if the artists models are centered or not
    public Vector3 GridToWorldMid(Vector2Int worldPos) => new Vector3(worldPos.x * tileSize.x, transform.position.y, worldPos.y * tileSize.y);

    public bool TryGetTile(int x, int y, out Tile tile)
    {
        tile = null;
        if (Tiles.InBounds(x, y) == false)
            return false;
        tile = Tiles.Get(x, y);
        return true;
    }

    public void Set(Fast2DArray<Tile> generated, Vector2 tileSize)
    {
        Tiles = generated;
        this.tileSize = tileSize;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnDrawGizmosSelected()
    {
        if (Tiles == null)
            return;

        Vector3 size = new Vector3(tileSize.x, 1, tileSize.y);

        for (int x = 0; x < Tiles.XSize; x++)
        {
            for (int y = 0; y < Tiles.YSize; y++)
            {
                Gizmos.color = Color.red;
                Vector3 pos = transform.position;
                pos.x = tileSize.x * x;
                pos.z = tileSize.y * y;
                Gizmos.DrawWireCube(pos, size);
            }
        }
    }

    public void PlaceTurret(Tower selectedTower, Tile tile)
    {
        Vector3 worldPos = GridToWorldMid(new Vector2Int(tile.X, tile.Y));
        tile.Tower = Instantiate(selectedTower, worldPos, Quaternion.identity);
    }
}
