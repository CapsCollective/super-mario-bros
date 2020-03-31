using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float horizontalSpeed;

    private bool active = false;
    private float currentHorizontalSpeed;
    private Rigidbody2D rb2d;
    private BoxCollider2D triggerCollider;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        triggerCollider = GetComponentInChildren<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovementController player;
        if (player = other.GetComponent<PlayerMovementController>())
        {
            // Decide which way the shell needs to move
            if (other.transform.position.x > transform.position.x)
            {
                horizontalSpeed = -Mathf.Abs(horizontalSpeed);
            } else
            {
                horizontalSpeed = Mathf.Abs(horizontalSpeed);
            }

            bool hitHead = true;

            // Toggle it on or off
            if (active && hitHead)
            {
                Deactivate();
                player.Bounce();
            }
            else if (hitHead)
            {
                Activate();
                player.Bounce();
            }
            else
            {
                player.Die();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool hitHead = false;

        foreach (ContactPoint2D cp2d in collision.contacts)
        {
            if (cp2d.normal == Vector2.up)
            {
                hitHead = true;
                break;
            }
        }

        PlayerMovementController player;
        NPCBehaviour npc;
        if ((player = collision.gameObject.GetComponent<PlayerMovementController>()) && !hitHead)
        {
            player.Die();
        } else if (npc = collision.gameObject.GetComponent<NPCBehaviour>())
        {
            npc.Kill(false);
        }
    }

    private void Update()
    {
        rb2d.velocity = new Vector2(currentHorizontalSpeed, rb2d.velocity.y);
    }

    private void Activate()
    {
        active = true;

        currentHorizontalSpeed = horizontalSpeed;
    }

    private void Deactivate()
    {
        active = false;

        currentHorizontalSpeed = 0f;
    }
}
