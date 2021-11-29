using UnityEngine;

public enum PlayerState {Idle, Moving, Attacking, Jumping, Dashing}

public class Player : MonoBehaviour
{
    public HealthBar healthBar;

    public GameObject gameOverCanvas;
    public GameObject player;
    public GameObject healthBarCanvas;
    public GameObject background;
    public GameObject background1;
    public GameObject background2;
    public GameObject playerShadow;

    public int maxHealth = 100;
    public int currentHealth;
    public int takeDamage = 10;
    public PlayerState state;

    public bool invulnerable = false;
    PlayerShoot playerShootScript;
    SpriteRenderer playerSpriteRenderer;
    PlayerMovement playerMovementScript;
    SpriteRenderer playerShadowSpriteRenderer;

    GameObject[] enemyGameObjects;

    void Start()
    {
        state = PlayerState.Idle;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerMovementScript = player.GetComponent<PlayerMovement>();
        playerShootScript = GetComponent<PlayerShoot>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerShadowSpriteRenderer = playerShadow.GetComponent<SpriteRenderer>();
        enemyGameObjects = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void TakeDamage(int damage)
    {
        if (invulnerable)
            return;

        if (currentHealth <= 0)
        {
            foreach (var enemy in enemyGameObjects)
            {
                enemy.SetActive(false);
            }

            gameOverCanvas.SetActive(true);
            healthBarCanvas.SetActive(false);
            background.SetActive(false);
            background1.SetActive(false);
            background2.SetActive(false);
            playerMovementScript.enabled = false;
            playerSpriteRenderer.enabled = false;
            playerShootScript.enabled = false;
            playerShadowSpriteRenderer.enabled = false;
            transform.position = new Vector3(0, 0, 0);
            playerShadow.transform.position = new Vector3(0, 0, 0);
        }

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
