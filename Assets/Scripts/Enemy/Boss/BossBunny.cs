using System;
using UnityEngine;

public class BossBunny : MonoBehaviour, IDamageable
{
    [SerializeField] private int health, maxHealth;
    public float chaseTimeMin, chaseTimeMax;
    public bool alive = true;

    [NonSerialized] public bool aggro;
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
        if (!alive) return;

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
