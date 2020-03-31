using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{

    [SerializeField] private int coinCount;

    public Text coinText;
    private LivesManager LM;
    private ScoreManager SM;

    private void Awake()
    {
        ResetCoinCount();
        LM = gameObject.GetComponent<LivesManager>();
        SM = gameObject.GetComponent<ScoreManager>();
    }

    public void AddCoins(int p)
    {
        coinCount += p;
        SM.AddPoints(200);
        UpdateCoinText();
    }

    public void ResetCoinCount()
    {
        coinCount = 0;
        SaveCoins();
        UpdateCoinText();
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("Coins", coinCount);
        PlayerPrefs.Save();
    }

    public void GetCoins()
    {
        PlayerPrefs.GetInt("Coins", 0);
    }

    private void CheckFor1UP()
    {
        if(coinCount>= 100)
        {
            if (LM)
            {
                LM.AddLives();
            }
            ResetCoinCount();
        }
    }

    public void UpdateCoinText()
    {
        CheckFor1UP();
        if (coinText)
        {
            coinText.text = ZerosGenerator(coinCount);
        }
        
    }

    private string ZerosGenerator(int score)
    {
        // depending on the size of the number, place zeros in front of the number before displaying it
        string zeros = "";
        for (int i = 1; i > 0; i--)
        {
            if (score / Mathf.Pow(10, i) < 1)
            {
                zeros += 0;
            }
        }
        return "x"+zeros + score.ToString();
    }
}
