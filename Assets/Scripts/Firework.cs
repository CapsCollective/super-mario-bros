using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    public ScoreManager SM;

    private void Awake()
    {
        SM = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreManager>();

    }


    public void Explode()
    {
        SM.AddPoints(500);
        // Play Animation here
    }

}
