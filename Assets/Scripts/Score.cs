using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance; // singleton

    public int scoreValue = 0; // was "public static int"
    public int highScore;

    Text score;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        score = GetComponent<Text>();
        scoreValue = 0;
    }

    public void AddScore(int addScore)
    {
        scoreValue += addScore;
        UpdateHighScore();
    }

    public void UpdateHighScore()
    {
        if(scoreValue > highScore)
        {
            highScore = scoreValue;
        }
    }

    private void Update()
    {
        score.text = "Score: " + scoreValue;
    }

}
