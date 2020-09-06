using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class representing a bullet, which not only inflicts
/// damage to the enemy it hits directly, but also to enemies in the near vicinity.
/// This works by getting all enemies in a certain radius and then proceed to apply damage as well.
/// This documentation is very good.
/// </summary>
public class AOEBullet : AbstractBullet
{
    // The damage all the other enemies in range of the aoe get inflicted.
    public int aoeDamage = 2;

    // The radius of the aoe / in which enemies are dealth damage. Range in Meters.
    public float aoeRadius = 1.5f;

    public override IEnumerator Move()
    {
        Vector3 dir = Target.transform.position - transform.position;
        dir = dir.normalized;
        while (true)
        {
            if (Target == null)
            {
                Destroy(gameObject);
                break;
            }

            this.transform.position += dir * Time.deltaTime * speed;
            yield return null;
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        // If collided with the target...
        if (other.gameObject.GetComponent<ITargetable>() == Target)
        {
            // ... give the target tower damage,...   
            Target.GetComponent<Health>().TakeDamage(damage);

            // ... get all other towers and damange them as well.
            Collider[] aoeColliders = Physics.OverlapSphere(Target.transform.position, aoeRadius);
            for (var i = 0; i < aoeColliders.Length; i++)
            {
                var collider = aoeColliders[i];
                if (collider.TryGetComponent<ITargetable>(out ITargetable tower))
                {
                    tower.GetComponent<Health>().TakeDamage(aoeDamage);
                }
            }

            Destroy(gameObject);
        }
    }
}