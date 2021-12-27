using UnityEngine;

public class BossBunny : MonoBehaviour
{
    [SerializeField] private int health, maxHealth;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
