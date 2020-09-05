using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public int Number { get; set; }

    public void Spawn(EnemyData data)
    {
        List<Tile> startPath = EnemyPathManager.Instance.GetStartPath(Number);
        GameObject enemyObject = Instantiate(data.model, transform.position, Quaternion.identity);
        // TODO: Instantiate Bennis 
        // enemyObject.AddComponent<Enemy>().Set(data, startPath);
    }
}
