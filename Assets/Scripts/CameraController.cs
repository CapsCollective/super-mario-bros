using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] float maxDistance = 5;
    [SerializeField] float minSpeed = 5;
    [SerializeField] [Range(0f, 0.5f)] float slowCamBound = 0.35f; 

    private float cameraSpeed = 0;
    private Transform playerTransform;
    private PlayerMovementController playerRb;
    private Camera cam;
    private Vector3 startPos;

    void Start()
    {
        cam = GetComponent<Camera>();
        startPos = transform.position;
        // Under the assumption that only one player exists
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerRb = playerTransform.GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var relativeScreenPos = cam.WorldToViewportPoint(playerTransform.position + new Vector3(0.5f, 0, 0));
        if (playerRb.CurrentAcceleration > 0.1f)
        {
            if (relativeScreenPos.x >= 0.5f)
            {
                //cameraSpeed = maxSpeed;
                transform.position = new Vector3(playerTransform.position.x + 0.5f, startPos.y, startPos.z);
            }
            //else if (relativeScreenPos.x > slowCamBound)
            //{
            //    //cameraSpeed = minSpeed;
            //    transform.position += new Vector3(minSpeed, 0, 0) * Time.deltaTime;
            //}
            else
                cameraSpeed = 0;

        }


        //    if (playerRb.CurrentAcceleration > 0f)
        //    {
        //        Vector3 dirToPlayer = (playerTransform.position - transform.position).normalized;
        //        var distance = Vector2.Distance(transform.position, playerTransform.position);
        //        var speed = Mathf.InverseLerp(maxDistance, 0, distance);
        //        transform.position += Vector3.right * (cameraSpeed * speed) * Time.deltaTime;

    }

}
