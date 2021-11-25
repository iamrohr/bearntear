using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar;

    public GameObject gameOverCanvas;
    public GameObject player;
    public GameObject healthBarCanvas;
    public GameObject background;
    public GameObject playerShadow;

    public int maxHealth = 100;
    public int currentHealth;
    public int takeDamage = 10;

    PlayerShoot playerShootScript;
    SpriteRenderer playerSpriteRenderer;
    PlayerMovement playerMovementScript;
    SpriteRenderer playerShadowSpriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerMovementScript = playerShadow.GetComponent<PlayerMovement>();
        playerShootScript = GetComponent<PlayerShoot>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerShadowSpriteRenderer = playerShadow.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            gameOverCanvas.SetActive(true);
            healthBarCanvas.SetActive(false);
            background.SetActive(false);
            playerMovementScript.enabled = false;
            playerSpriteRenderer.enabled = false;
            playerShootScript.enabled = false;
            playerShadowSpriteRenderer.enabled = false;
            transform.position = new Vector3(0, 0, 0);
            playerShadow.transform.position = new Vector3(0, 0, 0);
        }
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
