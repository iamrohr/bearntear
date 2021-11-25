using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseControlsCanvas;
    public GameObject pauseCanvas;

    private void Awake()
    {
        pauseControlsCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
    }

    public void PauseControls()
    {
        pauseControlsCanvas.SetActive(true);
    }

    public void PauseBack()
    {
        pauseControlsCanvas.SetActive(false);
    }

    public void PauseMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseQuit()
    {
        UnityEditor.EditorApplication.isPlaying = false; // needs to be replaced when Built

        // Application.Quit(); to be added to build
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                    Time.timeScale = 0; // ie. Pause
                    pauseCanvas.SetActive(true);

                    // player stops moving
            }

            else
            {
                    Time.timeScale = 1; // ie. Not Paused
                    pauseCanvas.SetActive(false);
            }
        }
    }
}

   