﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score;

    public Text scoreText;

    private void Awake()
    {
        ResetScore();
    }

    public void AddPoints(int p)
    {
        score += p;
        UpdateScoreText();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        if (scoreText)
        {
            scoreText.text = ZerosGenerator(score);
        }
        
    }

    private string ZerosGenerator(int score)
    {
        // depending on the size of the number, place zeros in front of the number before displaying it
        string zeros = "";
        for (int i = 5; i > 0; i--)
        {
            if (score / Mathf.Pow(10, i) < 1)
            {
                zeros += 0;
            }
        }
        return zeros + score.ToString();
    }


}