using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthResetSewer : MonoBehaviour
{
    GameObject player;
    Player playerScript;
    HealthBar healthBar;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();

        playerScript.currentHealth = 100;
        healthBar.SetHealth(100);
        
    }
}
