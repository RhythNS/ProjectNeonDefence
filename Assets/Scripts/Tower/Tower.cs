using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower :  Entity
{

    // Start is called before the first frame update
    void Start()
    {
        Health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
