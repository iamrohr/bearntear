using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
    GameObject[] enemyGameObjects;

    public GameObject controlsButton;
    public GameObject backButton;


    private void Awake()
    {
        pauseControlsCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        playerShadowSpriteRenderer = playerShadow.GetComponent<SpriteRenderer>();
        playerRenderer = player.GetComponent<SpriteRenderer>();
        playerShootScript = player.GetComponent<PlayerShoot>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        //playerShootScript.enabled = false;
        pauseControlsCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(backButton);

        projectileGameObjects = GameObject.FindGameObjectsWithTag("Projectile");
        
        foreach (var projectile in projectileGameObjects)
        {
            projectile.SetActive(false);
        }

        enemyGameObjects = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemyGameObjects)
        {
            enemy.SetActive(false);
        }


    }

    public void PauseBack()
    {
        pauseControlsCanvas.SetActive(false);
        //playerShootScript.enabled = false;
        pauseCanvas.SetActive(true);
        background.SetActive(true);
        background1.SetActive(true);
        background2.SetActive(true);
        playerShadowSpriteRenderer.enabled = true;
        playerRenderer.enabled = true;
        healthBarCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsButton);

        foreach (var projectile in projectileGameObjects)
        {
            projectile.SetActive(true);
        }

        foreach (var enemy in enemyGameObjects)
        {
            enemy.SetActive(true);
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
        if (Input.GetButtonDown("Cancel") && !pauseControlsCanvas.activeSelf)
        {
            if (Time.timeScale == 1)
            {
                    Time.timeScale = 0; // ie. Pause
                    pauseCanvas.SetActive(true);
                    playerShootScript.enabled = false;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(controlsButton);
                // player stops moving
            }

            else
            {
                    Time.timeScale = 1; // ie. Not Paused
                    pauseCanvas.SetActive(false);
                    playerShootScript.enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
            }
        }

        if(pauseControlsCanvas.activeSelf && Input.GetButtonDown("Cancel"))
        {
            PauseBack();
        }
    }
}

   