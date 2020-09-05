using MonoNet.Util.Datatypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MeeleAttackBehaviour : MonoBehaviour, Behaviour
{
    [SerializeField] private int range;
    private Enemy ownerEnemy;
    [FormerlySerializedAs("bulletPrefab")] [SerializeField] private MeeleBullet meeleBulletPrefab;

    [SerializeField] private float attackThreshold;
    public Tower AttackingTower { get; private set; }

    void Start()
    {
        ownerEnemy = GetComponent<Enemy>();
        StartCoroutine(UpdateAttack());
    }

    public void Set(MeleeAttackBehaviourData data)
    {
        attackThreshold = data.attackThreshold;
        range = data.range;
        meeleBulletPrefab = data.bulletPrefab;
    }

    private IEnumerator UpdateAttack()
    {
        while (true)
        {
            if (AttackingTower == null || !AttackingTower)
                yield return null;
            else
            {
                ShootMissileAt(AttackingTower);
                yield return new WaitForSeconds(attackThreshold);
            }
        }
    }

    private void ShootMissileAt(Tower attackingTower)
    {
        MeeleBullet spawnedMeeleBullet = Instantiate<MeeleBullet>(meeleBulletPrefab, transform.position, Quaternion.identity);
        spawnedMeeleBullet.Target = attackingTower;
        spawnedMeeleBullet.transform.LookAt(attackingTower.transform);
    }

    public void OnNewTileEntered()
    {
        Vector2Int currentGridPosition = World.Instance.WorldToGrid(transform.position);
        if (AttackingTower != null && AttackingTower)
        {
            if (CheckIfTargetStillInReach(currentGridPosition, World.Instance.WorldToGrid(AttackingTower.gameObject.transform.position)) == true)
            {
                return;
            }

        }

        Collider[] towerInSphere = Physics.OverlapSphere(transform.position, World.Instance.TileSize.x * range, 1 << 9);
        if (towerInSphere.Length == 0) return;
        Collider nearestCollider = towerInSphere[0];
        float minDistance = Vector3.SqrMagnitude(nearestCollider.transform.position - transform.position);
        float currDistance;
        for (int i = 1; i < towerInSphere.Length; i++)
        {
            currDistance = Vector3.SqrMagnitude(towerInSphere[i].transform.position - transform.position);
            if (minDistance > currDistance)
            {
                minDistance = currDistance;
                nearestCollider = towerInSphere[i];
            }
        }

        AttackingTower = nearestCollider.gameObject.GetComponent<Tower>();

    }

    private bool CheckIfTargetStillInReach(Vector2Int currentGridPosition, Vector2Int targetPosition)
        => Vector2Int.Distance(currentGridPosition, targetPosition) <= range;

    private void Attack(Tower tower)
    {
        AttackingTower = tower;
    }
}
