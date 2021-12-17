using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    Text highScoreText;
    private static int highScore;

    private void Start()
    {
        highScoreText = GetComponent<Text>();
        highScore = PlayerPrefs.GetInt("High Score", highScore);
        highScoreText.text = "High Score: " + highScore;
    }


}
