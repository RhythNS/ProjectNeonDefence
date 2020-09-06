using UnityEngine;

public class MeeleAttackBehaviour : AttackBehaviour
{
    public void Set(MeleeAttackBehaviourData data)
    {
        attackThreshold = data.attackThreshold;
        range = data.range;
        meeleBulletPrefab = data.bulletPrefab;
    }

    protected override void SetIdealTargetable(Collider[] towerInSphere)
    {
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

        AttackingTarget = nearestCollider.gameObject.GetComponent<Tower>();
    }
}
