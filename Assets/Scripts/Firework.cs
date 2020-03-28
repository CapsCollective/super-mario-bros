using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    public ScoreManager SM;
    public Animator myAnim;
    public SpriteRenderer mySR;

    private void Awake()
    {
        SM = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreManager>();
        myAnim = gameObject.GetComponent<Animator>();
        mySR = gameObject.GetComponent<SpriteRenderer>();

    }


    public void Explode()
    {
        SM.AddPoints(500);
        myAnim.Play("Explode");

        // TODO: Play audio of fireworks here /////////////////////////////////////////

        // Seems to destroy the soundguy GO
        //SoundGuy.Instance.PlaySound("smb_fireworks");
    }

    public void SpriteEnabled()
    {
        mySR.enabled = true;
    }

    public void SpriteDisabled()
    {
        mySR.enabled = false ;
    }

}
