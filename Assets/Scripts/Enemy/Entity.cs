using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Health))]

public abstract class Entity : ITargetable
{
    public Transform[] Aimpoint;

    public Health Health { get; protected set; }
}
