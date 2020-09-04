using MonoNet.Util.Datatypes;
using UnityEngine;

public class World : MonoBehaviour
{
    public static World Instance { get; private set; }

    public Fast2DArray<Tile> Tiles { get; private set; }

    [SerializeField] private Vector2 tileSize;

    public Vector2Int WorldToGrid(Vector3 worldPos) => new Vector2Int((int)(worldPos.x / tileSize.x), (int)(worldPos.y / tileSize.y));
    public Vector3 GridToWorld(Vector2Int worldPos) => new Vector3(worldPos.x * tileSize.x, worldPos.y * tileSize.y, transform.position.z);

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
                pos.y = tileSize.y * y;
                Gizmos.DrawWireCube(pos, size);
            }
        }
    }

}
