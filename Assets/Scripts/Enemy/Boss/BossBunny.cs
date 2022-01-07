using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossBunny : MonoBehaviour, IDamageable
{
    [SerializeField] private int health, maxHealth;
    public float chaseTimeMin, chaseTimeMax;
    public bool alive = true;
    [SerializeField] private AudioClip[] takeDamageSound;
    [SerializeField] private AudioClip deathSound;
    public GameObject[] animationHurt;

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

        AudioManager.Instance.sfxAudioSource.PlayOneShot(takeDamageSound[Random.Range(0, takeDamageSound.Length)], 0.8f);
        healthBar.SetHealth(health);
        health -= damage;
        if (health <= 0)
            Die();
        else
            stateManager.SwitchState(stateManager.HurtState);
    }

    private void Die()
    {
        AudioManager.Instance.sfxAudioSource.PlayOneShot(deathSound, 0.8f);
        stateManager.SwitchState(stateManager.DeadState);
        alive = false;
    }
}
