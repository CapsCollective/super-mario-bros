using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFlag : MonoBehaviour
{

    private BoxCollider2D myBC;

    [SerializeField]private Vector3 bottomFlagPos;
    [SerializeField] private float flagHeight;
    private float[] flagPointHeights = { 0.1f, 0.35f, 0.5f, 0.85f, 1.0f };
    private int[] flagPoints = {100, 400, 800, 2000, 5000};
    public enum FlagPositions { bottom, low, mid, high, top};

    public ScoreManager SM;
    public TimeManager TM;

    [SerializeField] private float timerPoint = 0f;
    public float rate = 5f;

    public Firework[] fireWorks;
    [SerializeField] private int fireworkCount = 0;

    private void Awake()
    {
        myBC = gameObject.GetComponent<BoxCollider2D>();
        SM = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreManager>();
        TM = SM.gameObject.GetComponent<TimeManager>();
        flagHeight = myBC.bounds.size.y;
        bottomFlagPos = gameObject.transform.position - flagHeight * 0.5f * Vector3.up;
    }

    public int FlagPolePositionPoints(float playerPosY, float playerHeight)
    {
        float playerFeetHeight = playerPosY - playerHeight* 0.5f;
        float distance = playerFeetHeight - bottomFlagPos.y;

        if (0f<=distance && distance < flagPointHeights[(int)FlagPositions.bottom]*flagHeight)
        {
            return flagPoints[(int)FlagPositions.bottom];
        }
        else if(flagPointHeights[(int)FlagPositions.bottom] * flagHeight <= distance && distance < flagPointHeights[(int)FlagPositions.low] * flagHeight)
        {
            return flagPoints[(int)FlagPositions.low];
        }
        else if (flagPointHeights[(int)FlagPositions.low] * flagHeight <= distance && distance < flagPointHeights[(int)FlagPositions.mid] * flagHeight)
        {
            return flagPoints[(int)FlagPositions.mid];
        }
        else if (flagPointHeights[(int)FlagPositions.mid] * flagHeight <= distance && distance < flagPointHeights[(int)FlagPositions.high] * flagHeight)
        {
            return flagPoints[(int)FlagPositions.high];
        }
        else if (flagPointHeights[(int)FlagPositions.high] * flagHeight <= distance && distance <= flagPointHeights[(int)FlagPositions.top] * flagHeight)
        {
            return flagPoints[(int)FlagPositions.top];
        }

        return 0;
    }

    public IEnumerator AddTimerToScore()
    {
        // reduce timer to zero incrementally via while loop whilst adding points
        TM.PauseTimer();
        fireworkCount = Mathf.FloorToInt(TM.GetTimer()) % 10;
        while (TM.IsTimeRemaining())
        {
            float timeAmount = Time.deltaTime * rate;
            TM.DecreaseTimer(timeAmount);
            timerPoint += timeAmount;
            if (timerPoint > 1f)
            {
                SM.AddPoints(50* Mathf.FloorToInt(timerPoint));
                timerPoint -= Mathf.Floor(timerPoint);
            }
            
            yield return null;   
        }
        FireWorks();
    }

    public void FireWorks()
    {
        // fireworks points for 1, 3, 6 for 500 points each
        
        if(fireworkCount==1 || fireworkCount == 3 || fireworkCount == 6)
        {

            StartCoroutine("ShootFireworks",fireworkCount);
            
        }

    }

    public IEnumerator ShootFireworks(int count)
    {
        
        for (int i = 0; i < count; i++)
        {
            // shoot firework, add 500 points at end of animation
            yield return new WaitForSeconds(1.0f);
            fireWorks[i].Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
             SM.AddPoints(FlagPolePositionPoints(collision.gameObject.transform.position.y, collision.bounds.size.y));
        }
        StartCoroutine("AddTimerToScore");
    }
}
