using System;
using UnityEngine;

public class BossBunny : MonoBehaviour, IDamageable
{
    [SerializeField] private int health, maxHealth;
    public float chaseTimeMin, chaseTimeMax;
    public bool alive = true;

    [NonSerialized] public bool aggro;
    private BossBunnyStateManager stateManager;

    [SerializeField] private HealthBar healthBar;
    [SerializeField] public GameObject healthBarObj;
    [SerializeField] public Animator healthAnimator;

    private void Awake()
    {
        stateManager = GetComponent<BossBunnyStateManager>();
    }

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (!alive) return;

        healthBar.SetHealth(health);
        health -= damage;
        if (health <= 0)
            Die();
        else
            stateManager.SwitchState(stateManager.HurtState);
    }

    private void Die()
    {
        stateManager.movement.StopMoving();
        stateManager.SwitchState(stateManager.DeadState);
        alive = false;
    }
}
