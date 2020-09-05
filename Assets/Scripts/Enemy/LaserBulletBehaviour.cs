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

            Vector3 dir = destination - transform.position;
            dir = dir.normalized;

            if (timer < shootingDuration)
            {
                //TODO set transform of cannonoutput
                lineBehaviour.StartPos =  dir * ( timer * speed);
            }
            else
            {
                transform.position += dir * Time.deltaTime * speed;
                
            }
            yield return null;
        }
    }

}
