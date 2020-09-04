using UnityEngine;

[CreateAssetMenu(fileName = "New WorldGen", menuName = "NeonDefence/WorldGenSettings")]
public class WorldGenSettings : ScriptableObject
{
    public int sizeX;
    public int sizeY;

    public float unblockablePercentange;

    public Vector2Int homePosition;
    public Vector2Int[] spawnPoints;
}
