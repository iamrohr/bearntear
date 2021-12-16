using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState { Idle, Moving, Attacking, Jumping, Dashing, Slamming }

public class Player : MonoBehaviour
{
    public int maxHealth = 100, currentHealth;
    public float invulnerableTime;
    public bool invulnerable = false;
    public int stage;
    public PlayerState state;
    public AudioSource damageSound;

    private PlayerFlash playerFlashScript;
    private PlayerJump playerJump;
    private HealthBar healthBar;

    [NonSerialized] public PlayerStateManager playerSM;
    [NonSerialized] public Animator animator;

    private void Awake()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        playerFlashScript = GetComponent<PlayerFlash>();
        playerJump = GetComponent<PlayerJump>();
        animator = GetComponent<Animator>();
        playerSM = GetComponent<PlayerStateManager>();
    }

    private void Start()
    {
        playerSM.EnterState(PlayerState.Idle);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (invulnerable)
            return;

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        MakeInvulnerable(invulnerableTime);
        StartCoroutine(playerFlashScript.Flash(0.05f, 10)); // 1st float x 2nd interger must equal invunerbleTime

        damageSound.Play();
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
            SceneManager.LoadScene("GameOver");
    }

    public void GetLife(int hp)
    {
        currentHealth += hp;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    public void SetStage(int stage)
    {
        this.stage = stage;
    }

    public void MakeInvulnerable(float time)
    {
        if (invulnerable)
            CancelInvoke(nameof(TurnOffInvulnerable));

        invulnerable = true;
        Invoke(nameof(TurnOffInvulnerable), time);
    }

    private void TurnOffInvulnerable()
    {
        invulnerable = false;
    }

}
