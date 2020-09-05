using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Behaviour))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    public Health Health;
    private List<Tile> path;

    [SerializeField] private Behaviour[] behaviour;

    public void Set(EnemyData data)
    {

    }
}
