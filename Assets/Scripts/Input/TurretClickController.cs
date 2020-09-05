using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretClickController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var gameObject = CameraController.Instance.GetSelectable();
            if(gameObject)Debug.Log(gameObject);
        }
    }
}
