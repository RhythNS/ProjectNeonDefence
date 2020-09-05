﻿using System.Collections;
using UnityEngine;

public class MeeleBullet : AbstractBullet
{
   /* [SerializeField] public float speed;
    [SerializeField] protected int damage;*/

    /*public Tower Target;

    protected Vector3 destination;*/

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

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Tower>() == Target)
        {
            Target.GetGameObject().GetComponent<Health>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }


}
