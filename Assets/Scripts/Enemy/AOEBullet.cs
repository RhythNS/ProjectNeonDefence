using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class representing a bullet, which not only inflicts
/// damage to the enemy it hits directly, but also to enemies in the near vicinity.
/// This works by getting all enemies in a certain radius and then proceed to apply damage as well.
/// This documentation is very good.
/// </summary>
public class AOEBullet : Bullet
{
   

   

    // The damage all the other enemies in range of the aoe get inflicted.
    public int aoeDamage = 2;

    // The radius of the aoe / in which enemies are dealth damage. Range in Meters.
    public float aoeRadius = 1.5f;




    public override void OnCollisionEnter(Collision collision)
    {
        // If collided with the target tower...
        if (collision.gameObject.GetComponent<Tower>() == Target)
        {
            // ... give the target tower damage,...   
            Target.GetComponent<Health>().TakeDamage(damage);
            
            // ... get all other towers and damange them as well.
            Collider[] aoeColliders = Physics.OverlapSphere(Target.Aimpoint.position, aoeRadius);
            foreach (var collider in aoeColliders)
            {
                if (!collider.TryGetComponent<Tower>(out Tower tower))
                {
                    tower.GetComponent<Health>().TakeDamage(aoeDamage);
                }
            }

        }
    }
}