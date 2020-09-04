using MonoNet.Util.Datatypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleAttackBehaviour : MonoBehaviour, Behaviour
{
    [SerializeField] private int checkRange;
    [SerializeField] private Enemy ownerEnemy;
    [SerializeField] private int damage;

    [SerializeField] private float attackThreshold;
    public Tower AttackingTower => attackingTower;
    private Tower attackingTower;

    void Start()
    {
        UpdateAttack();
    }

    private IEnumerator UpdateAttack()
    {
        while (true)
        {
            if (attackingTower == null || attackingTower)
                yield return null;
            attackingTower.Health.TakeDamage(damage);
            yield return new WaitForSeconds(attackThreshold);
        }
    }

    public void OnNewTileEntered(Tile tile)
    {
        Vector2Int currentGridPosition = World.Instance.WorldToGrid(ownerEnemy.transform.position);
        Fast2DArray<Tile> tiles = World.Instance.Tiles;
        Tile currentVisitedTile;
        int currentX = currentGridPosition.x;
        int currentY = currentGridPosition.y;
        for (int check = 0; check < checkRange; check++)
        {

            for (int x = 0; x < check * 2; x++)
            {
                for (int y = 0; y < check * 2; y++)
                {
                    currentVisitedTile = tiles.Get(currentX - check + x, currentY - check + y);
                    if(currentVisitedTile.Tower == null)
                    {
                        Attack(currentVisitedTile.Tower);               
                    }
                }
            }
        }
    }

    private void Attack(Tower tower)
    {
        ownerEnemy.Status = Enemy.AttackStatus.Attacking;
        attackingTower = tower;
    }
}
