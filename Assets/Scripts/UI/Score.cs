using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public static Score instance; // singleton

    public static int scoreValue = 0;
    public static int highScore;

    public GameObject inputFieldGameObject;
    public InputField inputText;
    string playerName;

    Text score;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        score = GetComponent<Text>();
        highScore = PlayerPrefs.GetInt("High Score", highScore);
        playerName = PlayerPrefs.GetString("Player Name");
        inputText.text = playerName;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main"))
        {
            inputFieldGameObject.SetActive(false);
        }
    }

    public void AddScore(int addScore)
    {
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
        // highScore = 0;
    }

    private void OnDestroy()
    {
        if (scoreValue >= highScore)
        {
            playerName = inputText.text;
            PlayerPrefs.SetString("Player Name", "Enter Name");
            PlayerPrefs.Save();
        }

        if (scoreValue < highScore)
        {
            playerName = inputText.text;
            PlayerPrefs.SetString("Player Name", playerName);
            PlayerPrefs.Save();
        }

        PlayerPrefs.SetInt("Score", scoreValue);
        PlayerPrefs.Save();

        PlayerPrefs.SetInt("High Score", highScore);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        score.text = "Score: " + scoreValue;

        if (scoreValue > highScore)
        {
            inputText.interactable = true;
        }

        if (scoreValue < highScore)
        {
            inputText.interactable = false;
        }
    }
}

