using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Health))]

public abstract class Entity : MonoBehaviour
{
    public Transform Aimpoint;

    public Health Health { get; protected set; }


}
