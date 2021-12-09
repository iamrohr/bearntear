using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance; // singleton

    public static int scoreValue = 0; // was "public static int"
    public static int highScore;

    Text score;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        score = GetComponent<Text>();
        
        highScore = PlayerPrefs.GetInt("High Score", highScore);
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
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Score", scoreValue);
        PlayerPrefs.Save();

        PlayerPrefs.SetInt("High Score", highScore);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        score.text = "Score: " + scoreValue;
    }

}
