using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject backgroundCanvas;
    public GameObject controlsCanvas;

    private void Start()
    {
        controlsCanvas.SetActive(false);
    }

    public void MainMenuStart()
    {
        SceneManager.LoadScene("Main");
    }

    public void MainMenuControls()
    {
        backgroundCanvas.SetActive(false);
        controlsCanvas.SetActive(true);
    }

    public void MainMenuBack()
    {
        backgroundCanvas.SetActive(true);
        controlsCanvas.SetActive(false);
    }

    public void MainMenuQuit()
    {
        UnityEditor.EditorApplication.isPlaying = false; // needs to be replaced when Built

        // Application.Quit(); to be added to build
    }

}
