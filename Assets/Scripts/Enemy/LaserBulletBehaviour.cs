using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class LaserBulletBehaviour : AbstractBullet
{
    [SerializeField] private float shootingDuration;

    private VolumetricLineBehavior lineBehaviour;

    [SerializeField] private float laserLength;

    private void Awake()
    {
        lineBehaviour = GetComponent<VolumetricLineBehavior>();
    }

    //public override IEnumerator Move()
    //{
    //    float timer = 0;
    //    lineBehaviour.EndPos = new Vector3(0, 0, 0);
    //    while (true)
    //    {
    //        timer += Time.deltaTime;

    //        Vector3 dir = Target.transform.position - transform.position;
    //        dir = dir.normalized;

    //        if (timer < shootingDuration)
    //        {
    //            lineBehaviour.StartPos = dir * (timer * speed);
    //        }
    //        else
    //        {
    //            transform.position += dir * Time.deltaTime * speed;
    //        }
    //        float angle = Vector2.Angle(transform.position + lineBehaviour.StartPos, Target.transform.position);
    //        Debug.Log("Angle " + angle);
    //        if (angle >= 180)
    //        {
    //            //Collision
    //            StartCoroutine(FadeAwayLaser());

    //            Target.GetComponent<Health>().TakeDamage(damage);
    //            yield break;
    //        }
    //        yield return null;
    //    }
    //}

    public override IEnumerator Move()
    {
        transform.rotation = Quaternion.identity;
        lineBehaviour.EndPos = new Vector3(0, 0, 0);
        Vector3 previousDir = Target.transform.position - transform.position;
        Vector3 lastSeenPosition = Target.transform.position;
        while (true)
        {
            if (Target)
            {
                lastSeenPosition = Target.transform.position;
            }
            

            Vector3 dir = lastSeenPosition - transform.position;
            dir = dir.normalized;

            Vector3 origin = shooter.transform.position;

            transform.position += dir * (speed * Time.deltaTime);

            float angle = Vector2.Angle(dir, previousDir);
            if (angle >= 50)
            {
                //Collision
                StartCoroutine(FadeAwayLaser());
                if(Target)
                    Target.GetComponent<Health>().TakeDamage(damage);
                yield break;
            }

            if (Vector3.Distance(origin, transform.position) < laserLength)
                lineBehaviour.StartPos = transform.InverseTransformPoint(origin);
            else
                lineBehaviour.StartPos = -dir * laserLength;

            previousDir = dir;

            yield return null;
        }
    }


    public IEnumerator FadeAwayLaser()
    {
        Vector3 dir = lineBehaviour.EndPos - lineBehaviour.StartPos;

        while (true)
        {
            lineBehaviour.StartPos += dir * speed * Time.deltaTime;

            Vector3 newDir = lineBehaviour.EndPos - lineBehaviour.StartPos;

            float angle = Vector2.Angle(dir, newDir);
            if (angle >= 50)
            {
                Destroy(gameObject);
                break;
            }

            yield return null;
        }

    }
}