using UnityEngine;

public class BossBunny : MonoBehaviour
{
    [SerializeField] private int health, maxHealth;
    public GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("PlayerHolder");
    }

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
