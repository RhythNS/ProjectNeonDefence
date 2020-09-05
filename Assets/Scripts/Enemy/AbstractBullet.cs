using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBullet : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] protected int damage;

    public ITargetable Target { get; set; }

    protected Vector3 destination;

    public void Awake()
    {
        destination = Target.GetCurrentPosition();
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
        GameObject targetObject = Target.GetGameObject();
        if (collision.gameObject == targetObject)
        {
            targetObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
