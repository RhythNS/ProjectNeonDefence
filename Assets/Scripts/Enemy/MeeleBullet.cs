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
        Vector3 dir = Target.GetCurrentPosition() - transform.position;
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
            Target.GetGameObject().GetComponent<Health>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }


}
