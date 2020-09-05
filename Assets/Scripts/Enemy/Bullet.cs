using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] protected int damage;

    public Tower Target;

    protected Vector3 destination;

    public void Awake()
    {
        destination = Target.Aimpoint.position;
        StartCoroutine(Move());
    }

    public void Update()
    {
        Vector3 pos = destination - transform.position;

        this.transform.position = pos.normalized * Time.deltaTime;
    }

    public IEnumerator Move()
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
            Target.GetComponent<Health>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }


}
