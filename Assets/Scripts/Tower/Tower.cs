using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Tower : Entity, ITargetable
{
    // Current Target 
    public Enemy targetEnemy;

    // Current list of all enemies in range
    // Is maintained by the GameManager
    public List<Enemy> enemiesInRange { get; private set; } = new List<Enemy>();


    public int Range;

    /// <summary>
    /// Adds the tower to the list of all awake towers.
    /// </summary>
    public void Awake()
    {
        GameManager.Instance.AliveTowers.Add(this);
        EnemyPathManager.Instance.OnWorldChange();
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Remove from the global list, as to not update it anymore
    /// </summary>
    public void OnDestroy()
    {
        GameManager.Instance.AliveTowers.Remove(this);
    }

    public Vector3 GetCurrentPosition()
    {
        return this ? transform.position : Vector3.zero;
    }


    public GameObject GetGameObject()
    {
        return this?gameObject:null;
    }

    /// <summary>
    /// The most basic implementation of targeting AI.
    /// Can be specified in sub-classes
    /// If the current target enemy is dead, the tower can pick a new target.
    /// 
    /// </summary>
    /// <param name="newTarget">If method return is true, the parameter will contain the new targeted enemy, else null</param>
    /// <returns></returns>
    public virtual bool TryUpdateEnemy(out Enemy newTarget)
    {
        // If target is dead, get new enemy
        if (!targetEnemy)
        {
            // We need to check if there even is a new enemy in range, if not, we dont need to bother.
            Enemy temporaryEnemy = GetNewTarget();
            if (temporaryEnemy != null)
            {
                // Setting the new enemy.
                targetEnemy = temporaryEnemy;
                newTarget = targetEnemy;
                return true;
            }
        }

        newTarget = null;
        return false;
    }

    protected virtual Enemy GetNewTarget()
    {
        if (enemiesInRange.Count == 0) return null;
        return enemiesInRange[0];
    }

    public virtual bool Upgrade()
    {
        return false;
    }

}