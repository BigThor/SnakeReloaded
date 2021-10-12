using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    private static int score = 0;
    public static int Score { get => score; }
    [SerializeField] private GameObject scoreText = null;

    // Start is called before the first frame update
    private void Start()
    {
        score = 0;
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.GetComponent<Text>().text = "Score: " + score.ToString();
    }
}
