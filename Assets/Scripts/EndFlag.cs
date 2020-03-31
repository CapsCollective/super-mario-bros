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

    public LevelCompleteManager LCM;

    

    private void Awake()
    {
        LCM = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelCompleteManager>();
        myBC = gameObject.GetComponent<BoxCollider2D>();
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

    

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //LCM.AddFlagPoints(FlagPolePositionPoints(collision.gameObject.transform.position.y, collision.bounds.size.y));
            LCM.Begin(FlagPolePositionPoints(collision.gameObject.transform.position.y, collision.bounds.size.y));
        }
    }
}
