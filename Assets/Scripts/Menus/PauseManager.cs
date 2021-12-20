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
    PlayerMovement playerMovementScript;
    PlayerInput playerInputScript;

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
        playerMovementScript = player.GetComponent<PlayerMovement>();
        playerInputScript = player.GetComponent<PlayerInput>();
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
        
        pauseCanvas.SetActive(false);
        playerShootScript.enabled = true;
        playerAttackScript.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        backgroundMusic.SetActive(true);
        buttonClickSound.Play();
        playerJumpScript.enabled = true;
        playerMovementScript.enabled = true;
        playerInputScript.enabled = true; // this is what causes jumping to occur after pressing pause
        Time.timeScale = 1; // ie. Not Paused
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
                playerMovementScript.enabled = false;
                playerInputScript.enabled = false;
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
                playerMovementScript.enabled = true;
                playerInputScript.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                backgroundMusic.SetActive(true);
            }
        }
    }
}

