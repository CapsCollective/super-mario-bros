using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        SoundGuy.Instance.PlaySound("smb_coin");
        //Todo: Increase score and coin counter
        Destroy(gameObject);
    }
}
