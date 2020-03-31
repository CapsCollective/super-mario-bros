using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Vector3 _startPos;
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        SoundGuy.Instance.PlaySound("smb_coin");
        StartCoroutine(Run());
    }
    
    private IEnumerator Run()
    {
        for (int i = 0; i < 32; i++)
        {
            transform.Translate(0,Mathf.Cos(i/10f)/4, 0);
            yield return null;
        }
        //Todo: Increase score and coin count
        Destroy(gameObject);
    }
}
