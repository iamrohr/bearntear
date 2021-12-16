using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Score : MonoBehaviour
{
    public static Score instance; // singleton

    public static int scoreValue = 0;
    public static int highScore;

    public GameObject inputFieldGameObject;
    //public GameObject startButton;

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
            //if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game Over"))
            //{
            //    if (scoreValue >= highScore)
            //    {
            //        inputFieldGameObject.SetActive(true);
            //        inputFieldGameObject.GetComponent<Image>().color = new Color32(4, 226, 253, 150);
            //        EventSystem.current.SetSelectedGameObject(null);
            //        EventSystem.current.SetSelectedGameObject(inputFieldGameObject);
            //    }

            //    if (scoreValue < highScore)
            //    {
            //        inputFieldGameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            //    }
            //}

            inputText.interactable = true; // this line works

            playerName = inputText.text;
            PlayerPrefs.SetString("Player Name", playerName); // "Enter Name"
            PlayerPrefs.Save();


        }

        if (scoreValue < highScore)
        {
            inputText.interactable = false; // this line works
        }
    }
}

