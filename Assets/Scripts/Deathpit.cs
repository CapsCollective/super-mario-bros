using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathpit : MonoBehaviour
{
    private GameController GC;

    private void Awake()
    {
        GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GC.MarioDie();
        }
    }
}
