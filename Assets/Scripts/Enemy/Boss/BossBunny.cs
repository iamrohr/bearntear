using UnityEngine;

public class BossBunny : MonoBehaviour, IDamageable
{
    [SerializeField] private int health, maxHealth;
    private BossBunnyStateManager stateManager;

    private void Awake()
    {
        stateManager = GetComponent<BossBunnyStateManager>();
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Destroy(gameObject);
        else
            stateManager.SwitchState(stateManager.HurtState);
    }
}
