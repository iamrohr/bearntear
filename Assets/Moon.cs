using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public GameObject lightMoon;
    public GameObject darkMoon;

    private void Update()
    {
        if(GameObject.FindGameObjectsWithTag("MiniBoss").Length <= 0 && Time.timeScale == 1)
        {
            lightMoon.SetActive(false);
            darkMoon.SetActive(true);
        }

    }
}
