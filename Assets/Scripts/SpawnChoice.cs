using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChoice : MonoBehaviour
{
    public GameObject RedMushroom;
    public GameObject FireFlower;
    void Start()
    {
        //TODO: Replace true with GameObject.FindWithTag("Player") and get their size
        Instantiate(true? RedMushroom : FireFlower , transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
