using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    public int lives;
    private int defaultLives = 3;

    public Text livesText;

    private void Awake()
    {
        lives = PlayerPrefs.GetInt("Lives", defaultLives);
        UpdateLivesText();
    }

    public void AddLives()
    {
            lives++;
    }

    public void LoseLives()
    {
        lives--;
        CheckLives();
    }

    public void CheckLives()
    {
        if(lives < 1)
        {
            GameOver();
        }
    }

    public void SetLives(int l)
    {
        PlayerPrefs.SetInt("Lives", l);
        PlayerPrefs.Save();
    }

    public void LevelComplete()
    {
        SetLives(lives);
    }

    private void GameOver()
    {
        SetLives(defaultLives);

        // run the gameover from the gameManager
    }

    public void UpdateLivesText()
    {
        if (livesText)
        {
            livesText.text = lives.ToString();
        }
    }
}
