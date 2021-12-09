using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MenuManager : MonoBehaviour
{
    public GameObject imageControlsGUI;
    public GameObject controlsCanvas;
    public GameObject gameOverCanvas;
    public GameObject imageGameOver;
    public GameObject startButton;
    public GameObject controlsBackButton;
    public AudioSource buttonClickSound;
    public Score scoreScript;
    public GameObject scoreCanvas;

    void Start()
    {
        scoreCanvas.SetActive(true);
        controlsCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    public void StartNewGame()
    {
        scoreScript.ResetScore();
        buttonClickSound.Play();
        SceneManager.LoadScene("Main");
    }

    public void ControlsMenu()
    {
        buttonClickSound.Play();
        controlsCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsBackButton);
    }

    public void MainMenu()
    {
        scoreCanvas.SetActive(false);
        scoreScript.ResetScore();
        buttonClickSound.Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        scoreScript.ResetScore();
        buttonClickSound.Play();

        //UnityEditor.EditorApplication.isPlaying = false; // needs to be replaced when Built
        Application.Quit(); //to be added to build
    }

    public void Back()
    {
        buttonClickSound.Play();
        gameOverCanvas.SetActive(true);
        controlsCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    



}
