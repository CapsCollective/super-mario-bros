using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public int lives;
    private int defaultLives = 3;

    private void Awake()
    {
        lives = PlayerPrefs.GetInt("Lives", defaultLives);
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
}
