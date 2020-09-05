using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : MonoBehaviour
{
    public static GameConstants Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public float TimeBetweenRounds { get => timeBetweenRounds; set => timeBetweenRounds = value; }
    [SerializeField] private float timeBetweenRounds;
    
    //--- Constants for Towers ---\\
    // Hard F Value for A*
    [SerializeField] private float towerHardFValue = 10;
    // The range in which a tower can attack
    [SerializeField] private float towerEffectiveRange = 5;
    // Cooldown between shots of bullets
    [SerializeField] private float towerBulletCooldownSeconds = 4;
    // How many towers are updated each frame, helps with performance.
    [SerializeField] private float towerManagerMaxTowersCheckedPerFrame = 3;
    
    public float TowerHardFValue => towerHardFValue;
    public float TowerEffectiveRange => towerEffectiveRange;
    public float TowerBulletCooldownSeconds => towerBulletCooldownSeconds;
    public float TowerManagerMaxTowersCheckedPerFrame => towerManagerMaxTowersCheckedPerFrame;

}
