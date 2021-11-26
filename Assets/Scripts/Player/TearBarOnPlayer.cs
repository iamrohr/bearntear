using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearBarOnPlayer : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentEnergy;

    //ndra denna
    public int currentHealth;

    public HealthBar tearBar;

    //Counter

    float timeElapsed = 100; //Count time down from 100 
    float timeSpeed = 10;    //Acceleration

    void Start()
    {
        currentHealth = maxHealth;
        tearBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        //decrease energy
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (currentEnergy >= 0)
            {
                timeElapsed -= timeSpeed * Time.deltaTime;
                currentEnergy = (int)timeElapsed;
                tearBar.SetHealth(currentHealth);

            }

        }

        //increase energy
        if (GameObject.FindGameObjectWithTag("Possessed") != null)
        {
            if (currentEnergy <= 100)
            {
                timeElapsed += timeSpeed * Time.deltaTime;
                currentHealth = (int)timeElapsed;
                tearBar.SetHealth(currentHealth);
            }

        }
    }
}