using UnityEngine;

[RequireComponent(typeof(IDieable))]
public class Health : MonoBehaviour
{
    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }
    public int DamageTaken => MaxHealth - CurrentHealth;

    public void Set(int health)
    {
        CurrentHealth = MaxHealth = health;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            GetComponent<IDieable>().Die();
            isDead = true;
        }
        else
        {
            isDead = false;
        }
    }

    public bool IsHead => isDead;
}
