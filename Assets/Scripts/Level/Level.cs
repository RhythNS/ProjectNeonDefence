using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "NeonDefence/Level")]
public class Level : ScriptableObject
{
    [SerializeField] private WorldGenSettings worldGenSettings;

    [SerializeField] private Wave[] waves;
}
