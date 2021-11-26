using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseControlsCanvas;
    public GameObject pauseCanvas;
    public GameObject background;
    public GameObject playerShadow;
    public GameObject player;
    public GameObject healthBarCanvas;

    SpriteRenderer playerShadowSpriteRenderer;
    SpriteRenderer playerRenderer;

    PlayerShoot playerShootScript;

    private void Awake()
    {
        pauseControlsCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        playerShadowSpriteRenderer = playerShadow.GetComponent<SpriteRenderer>();
        playerRenderer = player.GetComponent<SpriteRenderer>();
        playerShootScript = player.GetComponent<PlayerShoot>();
    }

    public void PauseControls()
    {
        background.SetActive(false);
        pauseCanvas.SetActive(false);
        playerShadowSpriteRenderer.enabled = false;
        playerRenderer.enabled = false;
        healthBarCanvas.SetActive(false);
        playerShootScript.enabled = false;
        pauseControlsCanvas.SetActive(true);
        

    }

    public void PauseBack()
    {
        pauseControlsCanvas.SetActive(false);
        playerShootScript.enabled = true;
        pauseCanvas.SetActive(true);
        background.SetActive(true);
        playerShadowSpriteRenderer.enabled = true;
        playerRenderer.enabled = true;
        healthBarCanvas.SetActive(true);

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

   