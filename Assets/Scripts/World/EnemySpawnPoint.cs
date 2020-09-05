using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public int Number { get; set; }

    public void Spawn(EnemyData data)
    {
        List<Tile> startPath = EnemyPathManager.Instance.GetStartPath(Number);
        GameObject enemyObject = Instantiate(data.model, transform.position, Quaternion.identity);
        enemyObject.AddComponent<EnemyDieable>();
        enemyObject.AddComponent<Health>();
        enemyObject.AddComponent<Enemy>().Set(data, startPath);
        for (int i = 0; i < data.behaviours.Length; i++)
        {
            if (data.behaviours[i] is MeleeAttackBehaviourData meleeAttack)
                enemyObject.AddComponent<MeeleAttackBehaviour>().Set(meleeAttack);
        }
    }
}
