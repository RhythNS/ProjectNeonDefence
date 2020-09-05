using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Tower : Entity, ITargetable
{
    // Current Target 
    //  public Enemy targetEnemy;
    public List<Enemy> targets;

    // Current list of all enemies in range
    // Is maintained by the GameManager
    public List<Enemy> enemiesInRange { get; private set; } = new List<Enemy>();

    // The range in which the tower can effectivly operate, aka. target enemies in and shoot.

    /// <summary>
    /// Adds the tower to the list of all awake towers.
    /// </summary>
    public void Awake()
    {
        GameManager.Instance.AliveTowers.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = GetComponent<Health>();
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
        return transform.position;
    }


    public GameObject GetGameObject()
    {
        return gameObject;
    }

    /// <summary>
    /// The most basic implementation of targeting AI.
    /// Can be specified in sub-classes
    /// If the current target enemy is dead, the tower can pick a new target.
    /// 
    /// </summary>
    /// <param name="newTarget">If method return is true, the parameter will contain the new targeted enemy, else null</param>
    /// <returns></returns>
    public virtual bool TryUpdateEnemy(out List<Enemy> newTarget)
    {
        // If target is dead, get new enemy
        if (targets.Count >= 0)
        {
            // We need to check if there even is a new enemy in range, if not, we dont need to bother.
            List<Enemy> newEnemies = GetNewTarget();
            if (newEnemies.Count > 0)
            {
                // Setting the new enemy.
                targets.AddRange(newEnemies);

                newTarget = targets;
                return true;
            }
        }

        newTarget = null;
        return false;
    }

    protected abstract List<Enemy> GetNewTarget();
    /* {
         if (enemiesInRange.Count == 0) return null;
         return enemiesInRange[0];
     }*/
}