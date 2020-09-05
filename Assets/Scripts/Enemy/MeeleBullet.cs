using System.Collections;
using UnityEngine;

public class MeeleBullet : AbstractBullet
{
    /* [SerializeField] public float speed;
     [SerializeField] protected int damage;*/

    /*public Tower Target;

    protected Vector3 destination;*/

    public override IEnumerator Move()
    {
        var targetObj = Target.GetGameObject();
        while (true)
        {
            if (!targetObj)
            {
                Destroy(gameObject);
                break;
            }

            var tPos = targetObj?.transform.position ?? Vector3.zero ;
            if (tPos == Vector3.zero) break;
            Vector3 dir = tPos - transform.position;
            dir = dir.normalized;
            this.transform.position += dir * Time.deltaTime * speed;
            yield return null;
        }

        /* 
         while (true)
         {
             this.transform.position += transform.forward * Time.deltaTime * speed;
             yield return null;
         }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ITargetable>() == Target)
        {
            Target?.GetGameObject().GetComponent<Health>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}