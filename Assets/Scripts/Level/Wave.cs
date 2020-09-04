using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "NeonDefence/Wave")]
public class Wave : ScriptableObject
{
    public Enemies[] enemies;

    [System.Serializable]
    public class Enemies
    {
        public int amount;
        public EnemyData enemyData;
    }
}