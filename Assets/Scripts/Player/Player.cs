using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState { Idle, Moving, Attacking, Jumping, Dashing, Slamming }

public class Player : MonoBehaviour
{
    public int maxHealth = 100, currentHealth;
    public float invulnerableTime;
    public bool invulnerable = false;
    public PlayerState state;
    public AudioSource damageSound;

    private PlayerFlash playerFlashScript;
    private PlayerJump playerJump;
    private HealthBar healthBar;

    [NonSerialized] public Animator animator;

    private void Awake()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        playerFlashScript = GetComponent<PlayerFlash>();
        playerJump = GetComponent<PlayerJump>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        EnterState(PlayerState.Idle);
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

        damageSound.Play();
        StartCoroutine(playerFlashScript.Flash());
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

    public void EnterState(PlayerState newState, float invTime = 0)
    {
        state = newState;

        switch (state)
        {
            case PlayerState.Idle:
                animator.SetTrigger("Idle");
                break;
            case PlayerState.Moving:
                animator.SetTrigger("Idle"); //Temp animation
                break;
            case PlayerState.Attacking:
                break;
            case PlayerState.Jumping:
                break;
            case PlayerState.Dashing:
                animator.SetTrigger("Dash");
                break;
            default:
                goto case PlayerState.Idle;
        }

        if (invTime > 0)
            MakeInvulnerable(invTime);
    }

    public void LeaveState(PlayerState state)
    {
        if (state != this.state) return;

        switch (state)
        {
            //case PlayerState.Idle:
            //    break;
            //case PlayerState.Moving:
            //    break;
            //case PlayerState.Attacking:
            //    break;
            //case PlayerState.Jumping:
            //    break;
            case PlayerState.Dashing:
                if (!playerJump.grounded)
                    EnterState(PlayerState.Jumping);
                else
                    goto default;
                break;
            default:
                EnterState(PlayerState.Idle);
                break;
        }
    }
}
