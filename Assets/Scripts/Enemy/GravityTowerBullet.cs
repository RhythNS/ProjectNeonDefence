using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTowerBullet : AOEBullet
{
    public void OnTriggerEnter(Collider other)
    {
        GameObject targetObject = Target.GetGameObject();
        if (other.gameObject == targetObject)
        {
            // ... get all other towers and damange them as well.
            Collider[] aoeColliders = Physics.OverlapSphere(Target.GetCurrentPosition(), aoeRadius);
            for (var i = 0; i < aoeColliders.Length; i++)
            {
                var collider = aoeColliders[i];
                if (collider.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    GravityTowerManager.Instance.AddGravity(enemy);
                }
            }

            Destroy(this.gameObject);
        }
    }
}