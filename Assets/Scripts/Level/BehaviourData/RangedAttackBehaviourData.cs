using UnityEngine;
using VolumetricLines;

[CreateAssetMenu(fileName = "New Ranged Attack Behaviour Data", menuName = "NeonDefence/RangedAttackBehaviourData")]
public class RangedAttackBehaviourData : BehaviourData
{
    public int Range;
    public float AttackThreshold;
    public VolumetricLineBehavior LaserPrefab;
    public MeeleBullet MeeleBullet;
}
