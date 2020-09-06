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
        Enemy e = enemyObject.AddComponent<Enemy>();
        e.Set(data, startPath);
        GameManager.Instance.AliveEnemies.Add(e);
        for (int i = 0; i < data.behaviours.Length; i++)
        {
            if (data.behaviours[i] is MeleeAttackBehaviourData meleeAttack)
                enemyObject.AddComponent<MeeleAttackBehaviour>().Set(meleeAttack);
            else if (data.behaviours[i] is RangedAttackBehaviourData rangedAttack)
                enemyObject.AddComponent<RangedAttackTankBehaviour>().Set(rangedAttack);
        }
    }
}
