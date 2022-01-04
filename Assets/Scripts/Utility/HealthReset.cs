using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReset : MonoBehaviour
{
    GameObject player;
    Player playerScript;
    HealthBar healthBar;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();

        if (playerScript.currentHealth < 50)
        {
            playerScript.currentHealth = 50;
            healthBar.SetHealth(50);
        }
    }
}
