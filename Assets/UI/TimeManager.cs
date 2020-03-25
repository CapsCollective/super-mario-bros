using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private float timer;
    private float origTimer = 400f;
    private bool pauseTimer = false;

    public Text timerText;

    private void Awake()
    {
        ResetTimer();
    }

    void Update()
    {
        if (!pauseTimer)
        {
            DecreaseTimer();
        }
    }

    private void DecreaseTimer()
    {
        timer -= Time.deltaTime;
        UpdateTimerText();
    }

    public void ResetTimer()
    {
        timer = origTimer;
        UpdateTimerText();
    }

    public void UpdateTimerText()
    {
        timerText.text = ZerosGenerator(Mathf.FloorToInt(timer));
    }

    public void PauseTimer()
    {
        pauseTimer = true;
    }

    public void UnpauseTimer()
    {
        pauseTimer = false;
    }

    private string ZerosGenerator(int score)
    {
        // depending on the size of the number, place zeros in front of the number before displaying it
        string zeros = "";
        for (int i = 2; i > 0; i--)
        {
            if (score / Mathf.Pow(10, i) < 1)
            {
                zeros += 0;
            }
        }
        return zeros + score.ToString();
    }
}
