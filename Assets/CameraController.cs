using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] float maxDistance = 5;
    [SerializeField] float cameraSpeed = 1;

    private Transform playerTransform;

    void Start()
    {

        // Under the assumption that only one player exists
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 dirToPlayer = (playerTransform.position - transform.position).normalized;
        var dot = Vector2.Dot(-Vector3.right, dirToPlayer);
        var distance = Vector2.Distance(transform.position, playerTransform.position);
        Debug.Log(dot);
        var speed = Mathf.InverseLerp(maxDistance, 0, distance);
        transform.position += Vector3.right * (cameraSpeed * speed) * Time.deltaTime;
    }

}
