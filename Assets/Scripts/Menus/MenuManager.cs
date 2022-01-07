using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MenuManager : MonoBehaviour
{
    private static MenuManager instance;
    public static MenuManager Instance { get { return instance; } }

    public GameObject imageControlsGUI;
    public GameObject controlsCanvas;
    public GameObject gameOverCanvas;
    public GameObject imageGameOver;
    public GameObject startButton;
    public GameObject controlsBackButton;
    public AudioSource buttonClickSound;
    public Score scoreScript;
    public GameObject scoreCanvas;
    public GameObject player;

    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        scoreCanvas.SetActive(true);
        controlsCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if(Score.scoreValue == 0 || Score.scoreValue < Score.highScore)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(startButton);
        }
    }

    public void StartNewGame()
    {
        scoreScript.ResetScore();
        buttonClickSound.Play();

        int lastScene = PersistentObject.Instance.lastScene;
        

        if (lastScene > 1)
        {
            SceneManager.LoadScene(lastScene);
            PersistentObject.Instance.gameObject.SetActive(true);
            Instantiate(player);
        }
        else
        {
            GameObject.Destroy(PersistentObject.Instance.gameObject);
            SceneManager.LoadScene(1);
        }
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
