using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState {Idle, Moving, Attacking, Jumping, Dashing}

public class Player : MonoBehaviour
{
    public HealthBar healthBar;
    public HealthBar tearBar;

    public GameObject player;
    public GameObject healthBarCanvas;
    public GameObject playerShadow;

    public int maxHealth = 100;
    public int currentHealth;
    public int takeDamage = 10;
    public PlayerState state;
    public bool invulnerable = false;

    //PlayerShoot playerShootScript; // isn't needed?
    SpriteRenderer playerSpriteRenderer;
    //PlayerMovement playerMovementScript; // isn't needed?
    SpriteRenderer playerShadowSpriteRenderer;

    void Start()
    {
        state = PlayerState.Idle;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        //playerMovementScript = player.GetComponent<PlayerMovement>(); // isn't needed?
        //playerShootScript = GetComponent<PlayerShoot>(); // isn't needed?
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerShadowSpriteRenderer = playerShadow.GetComponent<SpriteRenderer>();
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
    }

    public void GetLife(int hp)
    {
        currentHealth += hp;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        healthBar.SetHealth(currentHealth);
    }
}
