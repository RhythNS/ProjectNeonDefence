﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class LaserBulletBehaviour : AbstractBullet
{

    [SerializeField] private float shootingDuration;


    private VolumetricLineBehavior lineBehaviour;


    private void Awake()
    {
        lineBehaviour = GetComponent<VolumetricLineBehavior>();
    }


    public override IEnumerator Move()
    {

        float timer = 0;
        lineBehaviour.EndPos = new Vector3(0,0,0);
        while (true)
        {
            timer += Time.deltaTime;

            Vector3 dir = Target.GetCurrentPosition() - transform.position;
            dir = dir.normalized;

            if (timer < shootingDuration)
            {
                lineBehaviour.StartPos =  dir * ( timer * speed);
            }
            else
            {
                transform.position += dir * Time.deltaTime * speed;
                
            }
            if (Vector2.Angle(transform.position, Target.GetCurrentPosition()) >= 180)
            {
                //Collision
                StartCoroutine(FadeAwayLaser());
               
                Target.GetGameObject().GetComponent<Health>().TakeDamage(damage);
                yield break;
            }
            yield return null;
        }


    }


    public IEnumerator FadeAwayLaser()
    {
        float timer = 0;
        lineBehaviour.StartPos = Target.GetCurrentPosition();

        while (timer <= shootingDuration)
        {
            timer += Time.deltaTime;

            Vector3 dir = Target.GetCurrentPosition() - lineBehaviour.EndPos;
            dir = dir.normalized;

            lineBehaviour.EndPos += dir* speed * Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
    }

}
