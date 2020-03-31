using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum Direction
{
    None,
    Up,
    Down,
    Left,
    Right
}

public class Teleporter : MonoBehaviour
{
    public bool autoTrigger;
    public bool staticScreen;
    public Vector3 spawnPoint;
    public Vector3 cameraPos;
    public Direction enterDir;
    public Direction exitDir;

    private Camera _camera;
    
    public void Start() {
        _camera = Camera.main;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || !(Input.GetButtonDown("Down") || autoTrigger)) return;
        //Only trigger on player over and key down
        SoundGuy.Instance.PlaySound("smb_pipe");
        StartCoroutine(Teleport(other));
    }

    private IEnumerator Teleport(Collider2D player)
    {
        //TODO: Disable input
        player.GetComponent<Rigidbody2D>().simulated = false;

        switch (enterDir)
        {
            case Direction.Down:
                for (int i = 0; i < 80; i++)
                {
                    player.transform.Translate(0,-0.025f, 0);
                    yield return null;
                }
                break;
            case Direction.Left:
                for (int i = 0; i < 80; i++)
                {
                    player.transform.Translate(0.0125f,0, 0);
                    yield return null;
                }
                break;
        }
        player.transform.position = spawnPoint;
        _camera.transform.position = cameraPos;
        switch (exitDir)
        {
            case Direction.Up:
                for (int i = 0; i < 80; i++)
                {
                    player.transform.Translate(0, 0.025f, 0);
                    yield return null;
                }
                break;
            case Direction.None:
                break;
        }
        //TODO: Enable Input
        player.GetComponent<Rigidbody2D>().simulated = true;
        _camera.GetComponent<CameraController>().enabled = !staticScreen;
    }
    

}
