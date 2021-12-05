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

    private Animator animator;

    public PlayerFlash playerFlashScript;

    void Start()
    {
        animator = GetComponent<Animator>();
        state = PlayerState.Idle;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
<<<<<<< Updated upstream
=======
        //playerMovementScript = player.GetComponent<PlayerMovement>(); // isn't needed?
        //playerShootScript = GetComponent<PlayerShoot>(); // isn't needed?
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerShadowSpriteRenderer = playerShadow.GetComponent<SpriteRenderer>();
        
>>>>>>> Stashed changes
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
        StartCoroutine(playerFlashScript.Flash());
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
                break;
            case PlayerState.Attacking:
                break;
            case PlayerState.Jumping:
                break;
            case PlayerState.Dashing:
                animator.SetTrigger("Dash");
                break;
            default:
                break;
        }
    }
}
