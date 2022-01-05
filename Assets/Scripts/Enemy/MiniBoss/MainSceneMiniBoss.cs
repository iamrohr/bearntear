using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneMiniBoss : MonoBehaviour
{
    public GameObject backgroundMusic;
    public GameObject miniBossMusic;
    GameObject elevatorBlocker;

    GameObject nextLevel;

    private void Awake()
    {
        miniBossMusic.SetActive(false);
    }

    private void Start()
    {
        nextLevel = GameObject.FindGameObjectWithTag("NextLevel");
        elevatorBlocker = GameObject.Find("ElevatorBlocker");
        //if(nextLevel != null)
        //{
        //    nextLevel.SetActive(false);
        //}
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("MiniBoss").Length > 0)
        {
            miniBossMusic.SetActive(true);
            backgroundMusic.SetActive(false);
            elevatorBlocker.SetActive(true);
            //if (nextLevel != null)
            //{
            //    nextLevel.SetActive(false);
            //}
        }

        if (GameObject.FindGameObjectsWithTag("MiniBoss").Length <= 0 && Time.timeScale == 1)
        {
            miniBossMusic.SetActive(false);
            backgroundMusic.SetActive(true);
            elevatorBlocker.SetActive(false);
            //if (nextLevel != null)
            //{
            //    nextLevel.SetActive(true);
            //}
        }
    }
}
