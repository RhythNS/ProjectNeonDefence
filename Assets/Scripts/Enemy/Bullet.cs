using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private int damage;

    public Tower Target;

    private Vector3 destination;

    public void Awake()
    {
        destination = Target.Aimpoint.position;

        Move();
    }

    public void Update()
    {
        Vector3 pos = destination - transform.position;

        StartCoroutine(Move());

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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Tower>() == Target)
        {
            Target.GetComponent<Health>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }


}
