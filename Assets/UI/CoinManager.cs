using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{

    [SerializeField] private int coinCount;

    public Text coinText;
    public LivesManager LM;

    private void Awake()
    {
        ResetCoinCount();
        LM = gameObject.GetComponent<LivesManager>();
    }

    public void AddCoins(int p)
    {
        coinCount += p;
        UpdateCoinText();
    }

    public void ResetCoinCount()
    {
        coinCount = 0;
        UpdateCoinText();
    }

    private void CheckFor1UP()
    {
        if(coinCount>= 100)
        {
            // increment lives counter here
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
