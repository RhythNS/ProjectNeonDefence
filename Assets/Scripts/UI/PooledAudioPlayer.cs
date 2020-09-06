using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledAudioPlayer : MonoBehaviour
{
    public AudioClip[] clips;

    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        PlayRandomFromPool();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayRandomFromPool()
    {
        int rand = Random.Range(0, clips.Length);
        source.clip = clips[rand];
        source.Play();
    }
}
