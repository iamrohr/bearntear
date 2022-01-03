using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    public GameObject sewerCatBoss;
    public GameObject sewerRatBoss;
    public GameObject sewerBearBoss;

    public GameObject backgroundMusic;
    public GameObject miniBossMusic;

    private void Awake()
    {
        miniBossMusic.SetActive(false);
    }

    private void Update()
    {
        if (sewerCatBoss.activeInHierarchy || sewerRatBoss.activeInHierarchy || sewerBearBoss.activeInHierarchy)
        {
            miniBossMusic.SetActive(true);
            backgroundMusic.SetActive(false);
        }

        if (!sewerCatBoss.activeInHierarchy || !sewerRatBoss.activeInHierarchy || !sewerBearBoss.activeInHierarchy)
        {
            miniBossMusic.SetActive(false);
            backgroundMusic.SetActive(true);
        }
    }

}
