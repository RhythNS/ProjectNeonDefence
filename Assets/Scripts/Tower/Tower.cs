using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Tower : MonoBehaviour
{

    public Health Health { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
