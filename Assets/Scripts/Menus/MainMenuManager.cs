using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MainMenuManager : MonoBehaviour
{
    public GameObject backgroundCanvas;
    public GameObject creditsCanvas;
    public GameObject startButton;
    public GameObject creditsBackButton;
    public AudioSource buttonClickSound;

    private void Start()
    {
        Score.scoreValue = 0;
        creditsCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    public void MainMenuStart()
    {
        buttonClickSound.Play();
        SceneManager.LoadScene("Main");
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void MainMenuControls()
    {
        buttonClickSound.Play();
        backgroundCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void MainMenuCredits()
    {
        buttonClickSound.Play();
        backgroundCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsBackButton);

    }

    public void MainMenuBack()
    {
        buttonClickSound.Play();
        backgroundCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);

    }

    public void MainMenuCreditsBack()
    {
        buttonClickSound.Play();
        backgroundCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);

    }

    public void MainMenuQuit()
    {
        buttonClickSound.Play();
        //UnityEditor.EditorApplication.isPlaying = false; // needs to be replaced when Built

        Application.Quit(); //to be added to build
    }

    private void Update()
    {
        if (creditsCanvas.activeSelf && Input.GetButtonDown("Cancel"))
        {
            MainMenuBack();
        }
    }
}
