using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    private GameController GC;
    private int fireworkCount = 0;
    public float timeDecreaseRate = 5f;
    [SerializeField] private float timerPoint = 0f;
    public Firework[] fireWorks;

    private LowerFlag flag;
    private LowerPlayer lowPlayer;

    private void Awake()
    {
        
    }

    public void Begin()
    {
        FindObjects();
        StopPlayer();
        GC.TM.PauseTimer();
        fireworkCount = Mathf.FloorToInt(GC.TM.GetTimer()) % 10;
        flag.Lower();
        LowerPlayer();
        CastleEnter();

    }

    private void FindObjects()
    {
        flag = GameObject.FindGameObjectWithTag("Flagpole").GetComponentInChildren<LowerFlag>();
        lowPlayer = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<LowerPlayer>();
        GC = gameObject.GetComponent<GameController>();
        fireWorks = GameObject.FindGameObjectWithTag("Fireworks").GetComponentsInChildren<Firework>();
    }

    private void StopPlayer()
    {
        // TODO: Stop Mario controls and remove gravity
    }

    private void LowerPlayer()
    {
        if (lowPlayer)
        {
            lowPlayer.Lower();
        }
    }

    public void CastleEnter()
    {
        // TODO: Move mario into castle
        // Play victory sfx
        SoundGuy.Instance.PlaySound("smb_stage_clear");
    }

    public void AddFlagPoints(int flagPoints)
    {
        GC.SM.AddPoints(flagPoints);
    }

    public IEnumerator AddTimerToScore()
    {
        // reduce timer to zero incrementally via while loop whilst adding points
        
        while (GC.TM.IsTimeRemaining())
        {
            float timeAmount = Time.deltaTime * timeDecreaseRate;
            GC.TM.DecreaseTimer(timeAmount);
            timerPoint += timeAmount;
            if (timerPoint > 1f)
            {
                GC.SM.AddPoints(50 * Mathf.FloorToInt(timerPoint));
                timerPoint -= Mathf.Floor(timerPoint);
            }

            yield return null;
        }
        FireWorks();
    }

    public void FireWorks()
    {
        // fireworks points for 1, 3, 6 for 500 points each

        if (fireworkCount == 1 || fireworkCount == 3 || fireworkCount == 6)
        {

            StartCoroutine("ShootFireworks", fireworkCount);

        }

        // Returns to title screen upon point completion
        GC.LoadTitleScene();

    }

    public IEnumerator ShootFireworks(int count)
    {

        for (int i = 0; i < count; i++)
        {
            // shoot firework, add 500 points at end of animation
            yield return new WaitForSeconds(0.5f);
            fireWorks[i].Explode();
        }
    }
}
