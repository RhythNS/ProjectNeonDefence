using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TurretClickController : MonoBehaviour
{
    private bool hasClicked = false;
    public Material onClickMaterial;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (hasClicked) return;
            hasClicked = true;
            var gameObject = CameraController.Instance.GetSelectable();
            if (gameObject)
            {
                Debug.Log(gameObject);
                if (gameObject.TryGetComponent<Tile>(out Tile t))
                {
                    Debug.Log("Tile");
                    if (t.Tower)
                    {
                        if(t.Tower.Upgrade())Debug.Log("Upgraded!");
                        else Debug.Log("Not Upgraded!");
                    } 
                }
            }
                
        }
        else hasClicked = false;
    }
}