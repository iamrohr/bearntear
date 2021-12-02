using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MainMenuManager : MonoBehaviour
{
    public GameObject backgroundCanvas;
    public GameObject controlsCanvas;
    public GameObject creditsCanvas;
    public GameObject startButton;
    public GameObject controlsBackButton;
    public GameObject creditsBackButton;

    private void Start()
    {
        creditsCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);
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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsBackButton);
    }

    public void MainMenuCredits()
    {
        backgroundCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsBackButton);

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
        if (controlsCanvas.activeSelf && Input.GetButtonDown("Cancel") || creditsCanvas.activeSelf && Input.GetButtonDown("Cancel"))
        {
            MainMenuBack();
        }
    }
}
