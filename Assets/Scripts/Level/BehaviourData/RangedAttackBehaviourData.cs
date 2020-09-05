using UnityEngine;
using VolumetricLines;

[CreateAssetMenu(fileName = "New Ranged Attack Behaviour Data", menuName = "NeonDefence/RangedAttackBehaviourData")]
public class RangedAttackBehaviourData : ScriptableObject
{
    public int Range;
    public float AttackThreshold;
    public VolumetricLineBehavior laserPrefab;
}
