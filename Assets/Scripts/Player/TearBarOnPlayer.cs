using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearBarOnPlayer : MonoBehaviour
{
    public HealthBar tearBar;

    public int maxHealth = 100;
    public int currentHealth;
    
    //Counter
    float timeElapsed = 100; //Count time down from 100 
    public float timeSpeed = 1;    //Acceleration

    void Start()
    {
        currentHealth = maxHealth;
        tearBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        //decrease tear
        if (currentHealth > 0)
        { 
            timeElapsed -= timeSpeed * Time.deltaTime;
            currentHealth = (int)timeElapsed;
            tearBar.SetHealth(currentHealth);
        }
        
    }

}