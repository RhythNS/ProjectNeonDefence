using MonoNet.Util.Datatypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleAttackBehaviour : MonoBehaviour, Behaviour
{
    [SerializeField] private int range;
    [SerializeField] private Enemy ownerEnemy;
    [SerializeField] private Bullet bulletPrefab; 

    [SerializeField] private float attackThreshold;
    public Tower AttackingTower { get; private set; }

    void Start()
    {
        StartCoroutine(UpdateAttack());
    }

    private IEnumerator UpdateAttack()
    {
        while (true)
        {
            if (AttackingTower == null || AttackingTower)
                yield return null;
            ShootMissileAt(AttackingTower);
            yield return new WaitForSeconds(attackThreshold);
        }
    }

    private void ShootMissileAt(Tower attackingTower)
    {
        Bullet spawnedBullet = Instantiate<Bullet>(bulletPrefab);
        spawnedBullet.Target = attackingTower;
    }

    public void OnNewTileEntered(Tile tile)
    {
        Vector2Int currentGridPosition = World.Instance.WorldToGrid(ownerEnemy.transform.position);
        if(AttackingTower != null && AttackingTower)
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
       

        for (int check = 0; check < range; check++)
        {
            for (int x = 0; x < check * 2; x++)
            {
                //TODO Keine Redundanzen
                for (int y = 0; y < check * 2; y++)
                {
                    currentVisitedTile = tiles.Get(currentX - check + x, currentY - check + y);
                    if (currentVisitedTile.Tower == null)
                    {
                        Attack(currentVisitedTile.Tower);
                    }
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
