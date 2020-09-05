using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class MeeleAttackBehaviour : MonoBehaviour, IBehaviour
{
    [SerializeField] protected int range;
    [FormerlySerializedAs("bulletPrefab")] [SerializeField] protected AbstractBullet meeleBulletPrefab;

    [SerializeField] protected float attackThreshold;
    public ITargetable AttackingTarget { get; protected set; }

    public ITargetable GetCurrentTarget() => AttackingTarget;
    public void SetCurrentTarget(ITargetable targetable) => AttackingTarget = targetable;

    void Start()
    {
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
            if (AttackingTarget == null || !AttackingTarget.GetGameObject())
                yield return null;
            else
            {
                ShootMissileAt(AttackingTarget);
                yield return new WaitForSeconds(attackThreshold);
            }
        }
    }

    private void ShootMissileAt(ITargetable attackingTower)
    {
        MeeleBullet spawnedMeeleBullet = Instantiate(meeleBulletPrefab, transform.position, Quaternion.identity);
        spawnedMeeleBullet.Target = attackingTower;
        spawnedMeeleBullet.transform.LookAt(attackingTower.GetCurrentPosition());
    }

    public void OnNewTileEntered()
    {
        Vector2Int currentGridPosition = World.Instance.WorldToGrid(transform.position);
        if (AttackingTarget != null && !AttackingTarget.GetGameObject())
        {
            if (CheckIfTargetStillInReach(currentGridPosition, World.Instance.WorldToGrid(AttackingTarget.GetGameObject().transform.position)) == true)
            {
                return;
            }

        }

        Collider[] towerInSphere = Physics.OverlapSphere(transform.position, World.Instance.TileSize.x * range, 1 << 9);
        if (towerInSphere.Length == 0)
        {
            AttackingTarget = null;
            return;
        }

        SetIdealTargetable(towerInSphere);
    }

    protected virtual void SetIdealTargetable(Collider[] towerInSphere)
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

    private bool CheckIfTargetStillInReach(Vector2Int currentGridPosition, Vector2Int targetPosition)
        => Vector2Int.Distance(currentGridPosition, targetPosition) <= range;
}
