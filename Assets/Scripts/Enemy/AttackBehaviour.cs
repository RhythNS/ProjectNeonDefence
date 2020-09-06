using System.Collections;
using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour, IBehaviour
{
    [SerializeField] protected int range;

    [SerializeField] protected AbstractBullet meeleBulletPrefab;

    [SerializeField] protected float attackThreshold;
    public ITargetable AttackingTarget { get; protected set; }

    public ITargetable GetCurrentTarget() => AttackingTarget;
    public void SetCurrentTarget(ITargetable targetable) => AttackingTarget = targetable;

    void Start()
    {
        StartCoroutine(UpdateAttack());
    }

    private IEnumerator UpdateAttack()
    {
        while (true)
        {
            if (AttackingTarget == null || !AttackingTarget)
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
        AbstractBullet spawnedMeeleBullet = Instantiate(meeleBulletPrefab, transform.position, Quaternion.identity);
        spawnedMeeleBullet.Target = attackingTower;
        spawnedMeeleBullet.transform.LookAt(attackingTower.transform.position);
    }

    protected abstract AbstractBullet GetBullet();

    public void OnNewTileEntered()
    {
        Vector2Int currentGridPosition = World.Instance.WorldToGrid(transform.position);

        if (AttackingTarget != null && AttackingTarget)
        {
            if (CheckIfTargetStillInReach(currentGridPosition, World.Instance.WorldToGrid(AttackingTarget.transform.position)) == true)
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

    protected abstract void SetIdealTargetable(Collider[] towerInSphere);

    private bool CheckIfTargetStillInReach(Vector2Int currentGridPosition, Vector2Int targetPosition)
        => Vector2Int.Distance(currentGridPosition, targetPosition) <= range;
}
