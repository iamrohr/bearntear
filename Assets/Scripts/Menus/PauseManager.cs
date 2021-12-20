using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseCanvas;
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

    public GameObject backgroundMusic;
    public AudioSource buttonClickSound;

    private void Awake()
    {
        pauseCanvas.SetActive(false);
        playerShadowSpriteRenderer = playerShadow.GetComponent<SpriteRenderer>();
        playerRenderer = player.GetComponent<SpriteRenderer>();
        playerShootScript = player.GetComponent<PlayerShoot>();
        playerJumpScript = player.GetComponent<PlayerJump>();
        playerAttackScript = player.GetComponent<PlayerAttack>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        //UnityEditor.EditorApplication.isPlaying = false; //needs to be replaced when Built
        Application.Quit(); //to be added to build
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
        if (Input.GetButtonDown("Cancel"))
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
    }
}

