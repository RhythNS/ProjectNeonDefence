using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Tower Target;
    private Vector3 destination;
    private float speed;
    [SerializeField] private int damage;
    public float Speed { get => speed; set => speed = value; }

    public void Awake()
    {
        destination = Target.Aimpoint.position;

        Move();
    }

    public void Update()
    {
        Vector3 pos = destination - transform.position;


        this.transform.position = pos.normalized * Time.deltaTime;
    }

    public IEnumerable Move()
    {
        Vector3 dir = destination - transform.position;
        dir = dir.normalized;

        while (true)
        {
            this.transform.position = dir * Time.deltaTime * Speed;
            yield return null;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Tower>() == Target)
        {
            Target.GetComponent<Health>().TakeDamage(damage);
            Destroy(this.gameObject);
        } 
    }


}
