using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    public int lives;
    private int defaultLives = 3;

    public Text livesText;
    public GameController GC;

    private void Awake()
    {
        UpdateLivesText();
        GC = gameObject.GetComponent<GameController>();
    }

    public void AddLives()
    {
        lives++;
        SoundGuy.Instance.PlaySound("smb_1-up");
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

    public int GetLives()
    {
        return PlayerPrefs.GetInt("Lives", defaultLives);
    }

    public void SetLives(int l)
    {
        PlayerPrefs.SetInt("Lives", l);
        PlayerPrefs.Save();
    }

    public void ResetLives()
    {
        SetLives(defaultLives);
    }

    public void LevelComplete()
    {
        SetLives(lives);
    }

    private void GameOver()
    {
        SetLives(defaultLives);
        GC.GameOver();
        // run the gameover from the gameManager
    }

    public void UpdateLivesText()
    {
        lives = GetLives();

        if (livesText)
        {
            livesText.text = lives.ToString();
        }
    }
}
