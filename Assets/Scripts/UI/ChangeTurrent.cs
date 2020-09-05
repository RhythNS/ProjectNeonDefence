using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTurrent : MonoBehaviour
{
    [SerializeField] private Tower toChange;

    public void Change() => CameraController.Instance.SelectedTower = toChange;
}
