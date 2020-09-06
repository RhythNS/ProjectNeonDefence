using UnityEngine;
using VolumetricLines;

public class RangedAttackTankBehaviour : AttackBehaviour
{
    [SerializeField] private VolumetricLineBehavior laserPrefab;

    public void Set(RangedAttackBehaviourData data)
    {
        attackThreshold = data.AttackThreshold;
        range = data.Range;
        laserPrefab = data.LaserPrefab;
        meeleBulletPrefab = data.MeeleBullet;
    }

    protected override void SetIdealTargetable(Collider[] towerInSphere)
    {
        Collider furthest = towerInSphere[0];

        float maxDistance = Vector3.SqrMagnitude(furthest.transform.position - transform.position);
        float currDistance;
        for (int i = 1; i < towerInSphere.Length; i++)
        {
            currDistance = Vector3.SqrMagnitude(towerInSphere[i].transform.position - transform.position);
            if (maxDistance < currDistance)
            {
                maxDistance = currDistance;
                furthest = towerInSphere[i];
            }
        }

        AttackingTarget = furthest.gameObject.GetComponent<Tower>();
    }
}
