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
        {
            stateManager.SwitchState(stateManager.DeadState);
            alive = false;
        }
        else
            stateManager.SwitchState(stateManager.HurtState);
    }
}
