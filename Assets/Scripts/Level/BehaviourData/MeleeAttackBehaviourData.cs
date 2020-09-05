﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Attack Behaviour Data", menuName = "NeonDefence/MeleeAttackBehaviourData")]
public class MeleeAttackBehaviourData : ScriptableObject
{
    public int range;
    public float attackThreshold;
}
