using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRest : MonoBehaviour
{
    GameObject player;
    Player playerScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        
        if(playerScript.currentHealth < 50)
        {
            playerScript.currentHealth = 50;
        }
    }
}
