using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Behaviour))]
[RequireComponent(typeof(Health))]


public class Enemy : MonoBehaviour
{

    public Health Health;
    private List<Tile> path;

    public AttackStatus Status;

    [SerializeField] private Behaviour behaviour;
    
    

    
    

    public void Awake()
    {

    }

    public void Start()
    {
        
    }

    public void Update()
    {
        
    }


    private void SearchPath()
    {

    }

    public enum AttackStatus
    {
        NotAttacking,
        Attacking
    }
}
