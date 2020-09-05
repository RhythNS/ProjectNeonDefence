using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "NeonDefence/Level")]
public class Level : ScriptableObject
{
    public WorldGenSettings worldGenSettings;

    public Wave[] waves;
}
