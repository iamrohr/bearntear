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
        SceneManager.LoadScene("Main");
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

    

}
