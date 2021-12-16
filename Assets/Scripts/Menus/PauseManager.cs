using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseControlsCanvas;
    public GameObject pauseCanvas;
    //public GameObject backgroundTransparent;
    public GameObject backgroundTransparent2;

    public GameObject playerShadow;
    public GameObject player;
    public GameObject healthBarCanvas;

    SpriteRenderer playerShadowSpriteRenderer;
    SpriteRenderer playerRenderer;

    PlayerShoot playerShootScript;
    PlayerJump playerJumpScript;
    PlayerAttack playerAttackScript;

    GameObject[] projectileGameObjects;
    GameObject[] enemyGameObjects;

    public GameObject continueButton;
    public GameObject backButton;

    public GameObject backgroundMusic;
    public AudioSource buttonClickSound;


    private void Awake()
    {
        pauseControlsCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        playerShadowSpriteRenderer = playerShadow.GetComponent<SpriteRenderer>();
        playerRenderer = player.GetComponent<SpriteRenderer>();
        playerShootScript = player.GetComponent<PlayerShoot>();
        playerJumpScript = player.GetComponent<PlayerJump>();
        playerAttackScript = player.GetComponent<PlayerAttack>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseControls()
    {
        buttonClickSound.Play();
        //backgroundTransparent.SetActive(false);
        backgroundTransparent2.SetActive(false);
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
        buttonClickSound.Play();
        pauseControlsCanvas.SetActive(false);
        //playerShootScript.enabled = false;
        pauseCanvas.SetActive(true);
        //backgroundTransparent.SetActive(true);
        backgroundTransparent2.SetActive(true);
        playerShadowSpriteRenderer.enabled = true;
        playerRenderer.enabled = true;
        healthBarCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(continueButton);

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
        buttonClickSound.Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseQuit()
    {
        buttonClickSound.Play();
        //Invoke(nameof(Quit), 1f);
        UnityEditor.EditorApplication.isPlaying = false; // needs to be replaced when Built
        // Application.Quit(); to be added to build
    }

    public void Continue()
    {

        Time.timeScale = 1; // ie. Not Paused
        pauseCanvas.SetActive(false);
        playerShootScript.enabled = true;
        playerJumpScript.enabled = true;
        playerAttackScript.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        backgroundMusic.SetActive(true);
        buttonClickSound.Play();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && !pauseControlsCanvas.activeSelf)
        {
            if (Time.timeScale == 1)
            {
                    Time.timeScale = 0; // Paused
                    pauseCanvas.SetActive(true);
                    playerShootScript.enabled = false;
                    playerJumpScript.enabled = false;
                    playerAttackScript.enabled = false;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(continueButton);
                    backgroundMusic.SetActive(false);
                // player stops moving
            }

            else
            {
                    Time.timeScale = 1; // Not Paused
                    pauseCanvas.SetActive(false);
                    playerShootScript.enabled = true;
                    playerJumpScript.enabled = true;
                    playerAttackScript.enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    backgroundMusic.SetActive(true);
            }
        }

        if(pauseControlsCanvas.activeSelf && Input.GetButtonDown("Cancel"))
        {
            PauseBack();
        }
    }
}

   