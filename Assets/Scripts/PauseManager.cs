using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseControlsCanvas;
    public GameObject pauseCanvas;
    public GameObject background;
    public GameObject background1;
    public GameObject background2;

    public GameObject playerShadow;
    public GameObject player;
    public GameObject healthBarCanvas;

    SpriteRenderer playerShadowSpriteRenderer;
    SpriteRenderer playerRenderer;

    PlayerShoot playerShootScript;

    GameObject[] projectileGameObjects;

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
        background1.SetActive(false);
        background2.SetActive(false);
        pauseCanvas.SetActive(false);
        playerShadowSpriteRenderer.enabled = false;
        playerRenderer.enabled = false;
        healthBarCanvas.SetActive(false);
        playerShootScript.enabled = false;
        pauseControlsCanvas.SetActive(true);
        projectileGameObjects = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (var projectile in projectileGameObjects)
        {
            projectile.SetActive(false);
        }


    }

    public void PauseBack()
    {
        pauseControlsCanvas.SetActive(false);
        playerShootScript.enabled = true; // this should be disabled
        pauseCanvas.SetActive(true);
        background.SetActive(true);
        background1.SetActive(true);
        background2.SetActive(true);
        playerShadowSpriteRenderer.enabled = true;
        playerRenderer.enabled = true;
        healthBarCanvas.SetActive(true);

        foreach (var projectile in projectileGameObjects)
        {
            projectile.SetActive(true);
        }

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
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseControlsCanvas.activeSelf)
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

        if(pauseControlsCanvas.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseBack();
        }
    }
}

   