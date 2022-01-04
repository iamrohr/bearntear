using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniBoss : MonoBehaviour
{
    public GameObject backgroundMusic;
    public GameObject miniBossMusic;

    GameObject elevator;

    GameObject nextLevel;
    Animator elevatorAnimator;

    private void Awake()
    {
        miniBossMusic.SetActive(false);
    }

    private void Start()
    {
        nextLevel = GameObject.FindGameObjectWithTag("NextLevel");
        nextLevel.SetActive(false);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("RoofTop"))
        {
            elevator = GameObject.FindGameObjectWithTag("Elevator");
            elevatorAnimator = elevator.GetComponent<Animator>();
        }

    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("MiniBoss").Length > 0)
        {
            miniBossMusic.SetActive(true);
            backgroundMusic.SetActive(false);
            nextLevel.SetActive(false);
        }

        if (GameObject.FindGameObjectsWithTag("MiniBoss").Length <= 0 && Time.timeScale == 1)
        {
            miniBossMusic.SetActive(false);
            backgroundMusic.SetActive(true);
            
            nextLevel.SetActive(true);
        }

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("RoofTop"))
        {
            elevatorAnimator.SetTrigger("Open Doors");
        }
    }

}
