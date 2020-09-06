using UnityEngine;

public class DestroyOnDie : MonoBehaviour, IDieable
{
    public void Die() => Destroy(gameObject);
}
