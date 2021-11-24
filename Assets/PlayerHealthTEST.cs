using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthTEST : MonoBehaviour
{
    public HealthBar healthBar;

    public GameObject gameOverScreen;
    public GameObject player;
    public GameObject healthBarCanvas;
    public GameObject background;

    public int maxHealth = 100;
    public int currentHealth;
    public int takeDamage = 10;

    PlayerMovementTEST playerMovementTestScript;
    PlayerShoot playerShootScript;
    SpriteRenderer playerSpriteRenderer;

    void Start()
    {
        gameOverScreen.SetActive(false);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerMovementTestScript = GetComponent<PlayerMovementTEST>();
        playerShootScript = GetComponent<PlayerShoot>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            gameOverScreen.SetActive(true);
            healthBarCanvas.SetActive(false);
            background.SetActive(false);
            playerMovementTestScript.enabled = false;
            playerSpriteRenderer.enabled = false;
            playerShootScript.enabled = false;
            transform.position = new Vector3(0, 0, 0);
            // Destroy(player);

        }
        
        //if ()
        //    TakeDamage(takeDamage);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
