using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject imageControlsGUI;
    public GameObject controlsCanvas;
    public GameObject gameOverCanvas;
    public GameObject imageGameOver;

    void Start()
    {
        controlsCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("James");
    }

    public void ControlsMenu()
    {
        controlsCanvas.SetActive(true);
        imageGameOver.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false; // needs to be replaced when Built

        // Application.Quit(); to be added to build
    }

    public void Back()
    {
        gameOverCanvas.SetActive(true);
        controlsCanvas.SetActive(false);
        imageGameOver.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            // pause

            // player stops moving
            // time stops
            // buttons are shown - Controls, MainMenu, Quit

        }
    }
}
