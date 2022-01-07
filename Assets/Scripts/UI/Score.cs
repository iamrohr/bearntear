using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Score : MonoBehaviour
{
    public static Score instance; // singleton
    public static Score Instance { get { return instance; } }

    public static int scoreValue = 0;
    public static int highScore;

    public GameObject inputFieldGameObject;

    public InputField inputText;
    string playerName;

    public Text score;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        score = GetComponent<Text>();
        highScore = PlayerPrefs.GetInt("High Score", highScore);
        playerName = PlayerPrefs.GetString("Player Name");
        inputText.text = playerName;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Sewer") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("RoofTop"))
        {
            inputFieldGameObject.SetActive(false);
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameOver") && scoreValue >= highScore)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(inputFieldGameObject);
        }
    }

    public void AddScore(int addScore)
    {
        if (score == null)
            score = GetComponent<Text>();

        scoreValue += addScore;
        score.text = "Score: " + scoreValue;
        UpdateHighScore();
    }

    public void UpdateHighScore()
    {
        if(scoreValue > highScore)
        {
            highScore = scoreValue;
        }
    }

    public void ResetScore()
    {
        scoreValue = 0;
        // highScore = 0; to reset when testing as necessary
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Score", scoreValue);
        PlayerPrefs.Save();

        PlayerPrefs.SetInt("High Score", highScore);
        PlayerPrefs.Save();

        if (scoreValue >= highScore)
        {
            PlayerPrefs.SetString("Player Name", "Enter Name");
            PlayerPrefs.Save();
        }
    }

    private void Update()
    {
        score.text = "Score: " + scoreValue;

        if (scoreValue >= highScore)
        {
            inputText.interactable = true;
            
            playerName = inputText.text;
            PlayerPrefs.SetString("Player Name", playerName);
            PlayerPrefs.Save();
        }

        if (scoreValue < highScore)
        {
            inputText.interactable = false;
        }
    }
}

