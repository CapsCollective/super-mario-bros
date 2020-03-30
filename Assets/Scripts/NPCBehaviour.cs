using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(Rigidbody2D))]
public class NPCBehaviour : MonoBehaviour
{
    // Public variables
    public float horizontalSpeed;

    public float rayOffsetExcess;
    public float rayDistanceExcess;

    public bool avoidsFalling;
    public bool hostile;

    public Collider2D triggerCollider;
    public GameObject deathObject;

    public LayerMask raycastLayer;
    public LayerMask squashLayer;

    [Header("Debug Settings")]
    public bool debug;

    // Private variables
    private BoxCollider2D bc2d;
    private Rigidbody2D rb2d;

    // Properties
    private float XOffset { get { return bc2d.bounds.extents.x; } }
    private float YOffset { get { return bc2d.bounds.extents.y; } }

    private void Awake()
    {
        bc2d = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (debug)
        {
            Debug.Log("FLOOR: " + FloorCast());
            Debug.Log("FORWARD: " + ForwardCast());
        }

        if ((avoidsFalling && rb2d.velocity.y == 0 && !FloorCast()) || (ForwardCast())) horizontalSpeed *= -1f;
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(horizontalSpeed, rb2d.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // IF !hostile OR top of this collider IS LESS THAN OR EQUAL TO bottom of other collider

        Vector3 feet = other.transform.position - other.transform.up * other.bounds.extents.y;
        Vector3 head = transform.position + transform.up * triggerCollider.bounds.extents.y;

        bool hitHead = Vector3.Dot(transform.up, (feet - head).normalized) >= 0;

        if (!hostile || hitHead)
        {
            Die();
        }

        //if (!hostile || squashLayer == (squashLayer | (1 << collision.gameObject.layer)))
        //{
        //    Die();
        //}
    }

    public void Die()
    {
        if (deathObject) Instantiate(deathObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private bool ForwardCast()
    {
        Vector3 origin1 = bc2d.bounds.center - (transform.up * (YOffset + rayOffsetExcess));
        Vector3 origin2 = bc2d.bounds.center + (transform.up * (YOffset + rayOffsetExcess));
        Vector3 direction = transform.right * Mathf.Sign(horizontalSpeed);
        float distance = XOffset + rayDistanceExcess;

        bool hit = Physics2D.Raycast(origin1, direction, distance, raycastLayer) || Physics2D.Raycast(origin2, direction, distance, raycastLayer);

        if (debug)
        {
            Debug.DrawRay(origin1, direction * distance, Color.green);
            Debug.DrawRay(origin2, direction * distance, Color.green);
        }

        return hit;
    }

    private bool FloorCast()
    {
        Vector3 origin = bc2d.bounds.center + (transform.right * Mathf.Sign(horizontalSpeed) * (XOffset + rayOffsetExcess));
        Vector3 direction = -transform.up;
        float distance = YOffset + rayDistanceExcess;

        bool hit = Physics2D.Raycast(origin, direction, distance, raycastLayer);

        if (debug)
        {
            Debug.DrawRay(origin, direction * distance, Color.red);
        }

        return hit;
    }
}
