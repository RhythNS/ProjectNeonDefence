using UnityEngine;


[CreateAssetMenu(fileName = "New Ranged Attack Behaviour Data", menuName = "NeonDefence/RangedAttackBehaviourData")]
public class RangedAttackBehaviourData : BehaviourData
{
    public int Range;
    public float shootingDuration;
    public float AttackThreshold;
    public AbstractBullet BulletPrefab;
}
