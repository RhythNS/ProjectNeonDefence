using UnityEngine;
using VolumetricLines;

public class RangedAttackBehaviour : MeeleAttackBehaviour
{
    [SerializeField] private VolumetricLineBehavior laserPrefab;

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
