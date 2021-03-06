﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBullet : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] protected int damage;

    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioClip hitClip;

    public ITargetable Target
    {
        get => target;
        set
        {
            target = value;
            destination = Target.transform.position;
            StartCoroutine(Move());
        }
    }

    private void Start()
    {
        // Auto destroy after 5 seconds
        Destroy(gameObject, 5f);
    }

    protected Vector3 destination;
    private ITargetable target;

    public Transform shooter;

    public abstract IEnumerator Move();

    private void OnDestroy()
    {
        GameObject soundPlayer = new GameObject("Sound Player");

        AudioSource outS = soundPlayer.AddComponent<AudioSource>();
        AudioSource inS = soundPlayer.GetComponent<AudioSource>();

        outS.outputAudioMixerGroup = inS.outputAudioMixerGroup;
        outS.clip = hitClip;
        outS.Play();
        Destroy(soundPlayer, hitClip.length);
    }


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
