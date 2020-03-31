using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    void Start() {
        
        // Disable during spawn
        if(GetComponent<NPCBehaviour>()) GetComponent<NPCBehaviour>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        
        SoundGuy.Instance.PlaySound("smb_powerup_appears");
        StartCoroutine(Run());
    }
    
    private IEnumerator Run()
    {
        GetComponent<SpriteRenderer>().enabled = false;        
        for (int i = 0; i < 32; i++)
        {
            yield return null; // Wait for brick to be done bouncing
        }

        GetComponent<SpriteRenderer>().enabled = true;
        for (int i = 0; i < 80; i++)
        {
            transform.Translate(0,0.0125f, 0);
            yield return null;
        }

        GetComponent<Rigidbody2D>().simulated = true;
        if(GetComponent<NPCBehaviour>()) GetComponent<NPCBehaviour>().enabled = true;
    }
}
