using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SewerMiniBoss : MonoBehaviour
{
    public GameObject backgroundMusic;
    public GameObject miniBossMusic;

    private void Awake()
    {
        miniBossMusic.SetActive(false);
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("MiniBoss").Length > 0)
        {
            miniBossMusic.SetActive(true);
            backgroundMusic.SetActive(false);
        }

        if (GameObject.FindGameObjectsWithTag("MiniBoss").Length <= 0 && Time.timeScale == 1)
        {
            miniBossMusic.SetActive(false);
            backgroundMusic.SetActive(true);
        }
    }
}
