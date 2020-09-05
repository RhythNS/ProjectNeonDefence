using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackBehaviour : MonoBehaviour, Behaviour
{

    [SerializeField] private int range;

    [SerializeField] private LaserBullet laserPrefab;

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
        //TODO mit Noah besprechen, wie Laserbeam dargestellt werden soll? 


        LaserBullet laserBullet = Instantiate<LaserBullet>(laserPrefab);
        laserBullet.Target = attackingTower;
    }
}
