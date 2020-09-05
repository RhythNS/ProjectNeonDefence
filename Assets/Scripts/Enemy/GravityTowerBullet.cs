using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTowerBullet : AbstractBullet
{


    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public override IEnumerator Move()
    {
        Vector3 dir = destination - transform.position;
        dir = dir.normalized;

        while (true)
        {
            this.transform.position = dir * Time.deltaTime * speed;
            yield return null;
        }
    }
    
    public void OnCollisionEnter(Collision collision)
    {
        GameObject targetObject = Target.GetGameObject();
        if (collision.gameObject == targetObject)
        {
            targetObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
