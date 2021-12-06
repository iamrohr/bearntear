using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState {Idle, Moving, Attacking, Jumping, Dashing}

public class Player : MonoBehaviour
{
    public int maxHealth = 100, currentHealth;
    public bool invulnerable = false;
    public PlayerState state;
    public HealthBar healthBar, tearBar;
    public GameObject player, healthBarCanvas, playerShadow;
    public AudioSource damageSound;
    public PlayerFlash playerFlashScript;

    [NonSerialized] public Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        state = PlayerState.Idle;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (invulnerable)
            return;

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        damageSound.Play();
        StartCoroutine(playerFlashScript.Flash());
        animator.SetTrigger("Hurt");
    }

    public void GetLife(int hp)
    {
        currentHealth += hp;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    public void SwitchState(PlayerState newState)
    {
        state = newState;

        switch (state)
        {
            case PlayerState.Idle:
                animator.SetTrigger("Idle");
                break;
            case PlayerState.Moving:
                animator.SetTrigger("Idle"); //Temp anim state
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
    }
}
