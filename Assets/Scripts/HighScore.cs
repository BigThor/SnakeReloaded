using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();

        int lastHighScore = PlayerPrefs.GetInt("HighScore", -1);
        int currentScore = ScoreCounter.Score;

        if (lastHighScore < currentScore)
        {
            scoreText.GetComponent<Text>().text = "This is a new High Score !!!";
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
        }
        else
        {
            scoreText.GetComponent<Text>().text = "Your Highest Score is " + lastHighScore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
