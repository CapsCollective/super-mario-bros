using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScore : MonoBehaviour
{

    public ScoreManager SM;
    public TimeManager TM;
    public CoinManager CM;



    private void Awake()
    {
        SM.UpdateScoreText();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 60 == 0)
        {
            SM.AddPoints(3);
        }

        if (Time.frameCount % 10 == 0)
        {
            CM.AddCoins(1);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SM.ResetScore();
            TM.ResetTimer();
            CM.ResetCoinCount();
        }
    }
}
