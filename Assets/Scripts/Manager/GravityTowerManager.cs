using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class GravityTowerManager : MonoBehaviour
{
   /* private readonly Dictionary<Enemy, float> gravitiedEnemies = new Dictionary<Enemy, float>();
    private readonly Dictionary<Enemy, float> originalSpeeds = new Dictionary<Enemy, float>();*/
    private readonly List<Enemy> gravitiedEnemies = new List<Enemy>();
    public float slowDownCoefficient;
    public float maxGravitatedTimeSeconds;
    public static GravityTowerManager Instance;
    public GravityTower gravityTowerPref;

    public Material slowedDownMaterial;

    public Material normalSpeedMaterial;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
   /* void Update()
    {
        // Adds the delta time onto the gravity time
        float delta = Time.deltaTime;
        var keys = new List<Enemy>(gravitiedEnemies.Keys);
        for (var i = 0; i < keys.Count; i++)
        {
            var idx = keys[i];
            gravitiedEnemies[idx] += delta;
            // If the enemy has been gravitied for more than x seconds, remove the gravity.
            if (gravitiedEnemies[idx] > maxGravitatedTimeSeconds) RemoveGravity(idx);
        }
    }*/

    /// <summary>
    /// Adds the gravity effect to an enemy (Slows them down)
    /// </summary>
    /// <param name="e"></param>
    public void AddGravity(Enemy e, SlowdownStatusEffect effect)
    {
       
       
    }


    

    /// <summary>
    /// Removes the gravity effect from an enemy (speeds them up again)
    /// </summary>
    /// <param name="e"></param>
    /*public void RemoveGravity(Enemy e)
    {
        if (gravitiedEnemies.ContainsKey(e))
        {
            Debug.Log("Unfreezing Enemy " + e.ToString());
            gravitiedEnemies.Remove(e);
            e.SpeedForPassingTile = originalSpeeds[e];
            originalSpeeds.Remove(e);
            e.GetComponent<Renderer>().material = normalSpeedMaterial;
        }
    }*/
}