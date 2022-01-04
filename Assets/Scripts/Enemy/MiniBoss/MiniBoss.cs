using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        elevator = GameObject.FindGameObjectWithTag("Elevator");
        elevatorAnimator = elevator.GetComponent<Animator>();

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
            elevatorAnimator.SetTrigger("Open Doors");
            nextLevel.SetActive(true);
        }
    }

}
