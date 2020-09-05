using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Attack Behaviour Data", menuName = "NeonDefence/MeleeAttackBehaviourData")]
public class MeleeAttackBehaviourData : BehaviourData
{
    public MeeleBullet bulletPrefab;
    public int range;
    public float attackThreshold;
}
