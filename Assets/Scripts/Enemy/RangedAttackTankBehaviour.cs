using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class RangedAttackBehaviour : MonoBehaviour, Behaviour
{

    [SerializeField] private int range;

    [SerializeField] private VolumetricLineBehavior laserPrefab;

    [SerializeField] private float attackThreshold;
    public Tower AttackingTower { get; private set; }


    public void Set()
    {
        //TODO @Noah/RhythNS
        //attackThreshold = data.attackThreshold;
        //range = data.range;
    }

    public void OnNewTileEntered(Tile tile)
    {
        Vector2Int currentGridPosition = World.Instance.WorldToGrid(transform.position);
        if (AttackingTower != null && AttackingTower)
        {
            if (CheckIfTargetStillInReach(currentGridPosition, World.Instance.WorldToGrid(AttackingTower.gameObject.transform.position)) == true)
            {
                return;
            }
        }
        Collider[] towerInSphere = Physics.OverlapSphere(transform.position, range, 1 << 9);
        Collider furthestCollider = towerInSphere[0];
        float maxDistance = Vector3.SqrMagnitude(furthestCollider.transform.position - transform.position);
        float currDistance;
        for (int i = 1; i < towerInSphere.Length; i++)
        {
            currDistance = Vector3.SqrMagnitude(towerInSphere[i].transform.position - transform.position);
            if (maxDistance < currDistance)
            {
                maxDistance = currDistance;
                furthestCollider = towerInSphere[i];
            }
        }

        AttackingTower = furthestCollider.gameObject.GetComponent<Tower>();

        //TODO rotationAnimation
    }


    private IEnumerator UpdateAttack()
    {
        while (true)
        {
            if (AttackingTower == null || !AttackingTower)
                yield return null;
            ShootMissileAt(AttackingTower);
            yield return new WaitForSeconds(attackThreshold);
        }
    }

    private void ShootMissileAt(Tower attackingTower)
    {
        VolumetricLineBehavior lineBehaviour = Instantiate(laserPrefab);
        LaserBulletBehaviour laserBullet = lineBehaviour.gameObject.AddComponent<LaserBulletBehaviour>();
        laserBullet.Target = attackingTower;
        laserBullet.transform.LookAt(attackingTower.transform);
        



    }

    private bool CheckIfTargetStillInReach(Vector2Int currentGridPosition, Vector2Int targetPosition)
        => Vector2Int.Distance(currentGridPosition, targetPosition) <= range;

}
