using System.Collections;
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

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", score);
    }


    public int GetScore()
    {
        return PlayerPrefs.GetInt("Score", 0);
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
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
