using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int currentHealth, maxHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}
