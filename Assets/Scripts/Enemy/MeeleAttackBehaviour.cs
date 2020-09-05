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
        MeeleBullet spawnedMeeleBullet = Instantiate<MeeleBullet>(meeleBulletPrefab);
        spawnedMeeleBullet.Target = attackingTower;
        spawnedMeeleBullet.transform.LookAt(attackingTower.transform);
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
        Fast2DArray<Tile> tiles = World.Instance.Tiles;
        Tile currentVisitedTile;
        int currentX = currentGridPosition.x;
        int currentY = currentGridPosition.y;

        for (int i = 0; i < range; i++)
        {
            for (int j = -i; j <= i; j++)
            {
                //Check row above
                if(World.Instance.TryGetTile(currentX - i, currentY + j, out currentVisitedTile) && currentVisitedTile.Tower != null)
                {
                    Attack(currentVisitedTile.Tower);
                    return;
                }

                //Check row below
                if(World.Instance.TryGetTile(currentX + i, currentY + j, out currentVisitedTile) && currentVisitedTile.Tower != null)
                {
                    Attack(currentVisitedTile.Tower);
                    return;
                }
                //Check row below
                if(World.Instance.TryGetTile(currentX + j, currentY - i, out currentVisitedTile) && currentVisitedTile.Tower != null)
                {
                    Attack(currentVisitedTile.Tower);
                    return;
                }
                //Check row below
                if(World.Instance.TryGetTile(currentX + j, currentY + i, out currentVisitedTile) && currentVisitedTile.Tower != null)
                {
                    Attack(currentVisitedTile.Tower);
                    return;
                }
            }
        }

    }

    private bool CheckIfTargetStillInReach(Vector2Int currentGridPosition, Vector2Int targetPosition)
        => Vector2Int.Distance(currentGridPosition, targetPosition) <= range;

    private void Attack(Tower tower)
    {
        AttackingTower = tower;
    }
}
