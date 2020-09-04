using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool CanBuildOn { get; private set; }
    public Tower Tower { get; set; }
}
