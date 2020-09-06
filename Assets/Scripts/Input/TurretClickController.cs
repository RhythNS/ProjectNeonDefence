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
            var gameObject = GetSelectedTower();
            if (gameObject)
            {
                Tower tower = gameObject.GetComponent<Tower>();
                if (tower)
                {
                    if(tower.Upgrade())Debug.Log("Upgraded!");
                    else Debug.Log("Not upgraded.");
                }
            }
        }
        else hasClicked = false;
    }


    public GameObject GetSelectedTower()
    {
        if (GetHitAtMousePos(out RaycastHit hit) == false || hit.collider.gameObject.GetComponent<Tower>() == null)
            return null;
        return hit.collider.gameObject;
    }

    private bool GetHitAtMousePos(out RaycastHit hit)
    {
        Ray ray = CameraController.Instance.AttachedCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2);

        return Physics.Raycast(ray, out hit, 100f);
    }
}