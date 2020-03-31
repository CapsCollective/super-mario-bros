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
    private PlayerMovementController PMC;

    private void Awake()
    {
        
    }

    public void Begin(int flagpoints)
    {
        FindObjects();
        GC.SM.AddPoints(flagpoints);
        StopPlayer();
        GC.TM.PauseTimer();
        fireworkCount = Mathf.FloorToInt(GC.TM.GetTimer()) % 10;
        flag.Lower();
        LowerPlayer();
        //CastleEnter();

    }

    private void FindObjects()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PMC = player.GetComponent<PlayerMovementController>();
        lowPlayer = player.GetComponent<LowerPlayer>();
        flag = GameObject.FindGameObjectWithTag("Flagpole").GetComponentInChildren<LowerFlag>();
        lowPlayer = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<LowerPlayer>();
        GC = gameObject.GetComponent<GameController>();
        fireWorks = GameObject.FindGameObjectWithTag("Fireworks").GetComponentsInChildren<Firework>();
    }

    private void StopPlayer()
    {
        // TODO: Stop Mario controls and remove gravity
        PlayerMovementController.InputEnabled = true;
        PMC.enabled = false;
    }

    public void LowerPlayer()
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
        PMC.enabled = true;
        PlayerMovementController.InputEnabled = false;
        PlayerMovementController.AutoMoveDir = 1;
        SoundGuy.Instance.PlaySound("smb_stage_clear");
    }

    public void AddFlagPoints(int flagPoints)
    {
        GC.SM.AddPoints(flagPoints);
    }

    public IEnumerator AddTimerToScore()
    {
        while (GC.TM.IsTimeRemaining())
        {
            float timeAmount = Time.deltaTime * timeDecreaseRate;
            GC.TM.DecreaseTimer(timeAmount);
            timerPoint += timeAmount;
            if (timerPoint > 1f)
            {
                GC.SM.AddPoints(50 * Mathf.FloorToInt(timerPoint));
                timerPoint -= Mathf.Floor(timerPoint);
                
                //TODO: Timer countdown sound

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
        else
        {
            
            PlayerMovementController.InputEnabled = true;
            GC.LoadTitleScene();
        }
    }

    public IEnumerator ShootFireworks(int count)
    {

        for (int i = 0; i < count; i++)
        {
            // shoot firework, add 500 points at end of animation
            yield return new WaitForSeconds(0.5f);
            fireWorks[i].Explode();
        }

        yield return new WaitForSeconds(1.0f);
        PlayerMovementController.InputEnabled = true;
        GC.LoadTitleScene();
    }
}
