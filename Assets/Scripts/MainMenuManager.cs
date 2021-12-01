using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject backgroundCanvas;
    public GameObject controlsCanvas;
    public GameObject creditsCanvas;

    private void Start()
    {
        creditsCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
    }

    public void MainMenuStart()
    {
        SceneManager.LoadScene("Main");
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void MainMenuControls()
    {
        backgroundCanvas.SetActive(false);
        controlsCanvas.SetActive(true);
    }

    public void MainMenuCredits()
    {
        backgroundCanvas.SetActive(false);
        creditsCanvas.SetActive(true);

    }

    public void MainMenuBack()
    {
        backgroundCanvas.SetActive(true);
        controlsCanvas.SetActive(false);

    }

    public void MainMenuCreditsBack()
    {
        backgroundCanvas.SetActive(true);
        creditsCanvas.SetActive(false);

    }

    public void MainMenuQuit()
    {
        UnityEditor.EditorApplication.isPlaying = false; // needs to be replaced when Built

        // Application.Quit(); to be added to build
    }

    private void Update()
    {
        if (controlsCanvas.activeSelf && Input.GetKeyDown(KeyCode.Escape) || creditsCanvas.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenuBack();
        }
    }
}
