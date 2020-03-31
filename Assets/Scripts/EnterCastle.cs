using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCastle : MonoBehaviour
{
    public LevelCompleteManager LCM;

    private void Awake()
    {
        LCM = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelCompleteManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LCM.AddTimerToScore();
            SpriteRenderer SR = collision.gameObject.GetComponent<SpriteRenderer>();
            SR.enabled = false;
            PlayerMovementController PMC = collision.gameObject.GetComponent<PlayerMovementController>();
            PMC.enabled = false;
            StartCoroutine(LCM.AddTimerToScore());
            // Hide player sprite and stop movement
        }
    }
}
