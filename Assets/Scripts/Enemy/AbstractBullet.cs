using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBullet : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] protected int damage;

    public ITargetable Target
    {
        get => target;
        set
        {
            target = value;
            destination = Target.GetCurrentPosition();
            StartCoroutine(Move());
        }
    }

    private void Start()
    {
        // Auto destroy after 5 seconds
        Destroy(gameObject,5);
    }

    protected Vector3 destination;
    private ITargetable target;

    public abstract IEnumerator Move();

    //public virtual void OnCollisionEnter(Collision collision)
    //{
    //    GameObject targetObject = Target.GetGameObject();
    //    if (collision.gameObject == targetObject)
    //    {
    //        targetObject.GetComponent<Health>().TakeDamage(damage);
    //        Destroy(this.gameObject);
    //    }
    //}
}
