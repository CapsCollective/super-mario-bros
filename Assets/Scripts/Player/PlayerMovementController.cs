using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    // The default walk speed
    [SerializeField] private float walkSpeed = 5;
    // How high the jump will go
    [SerializeField] private float jumpMultiplier = 5;
    // The speed of the jump
    // Changing this also changes the height of the jump as more or less gravity will be added
    [SerializeField] private float jumpSpeed = 5;
    // The offset of where the ground will be checked
    [SerializeField] private float groundOffset;
    // How far the ground will be checked
    [SerializeField] private float groundDistance;
    // The curve used to determine how much gravity will be applied for the jump.
    [SerializeField] private AnimationCurve jumpCurve;
    // The scale of the gravity when the player is falling
    [SerializeField] private float gravityScale = -.1f;
    [SerializeField] private float maxGravityDownForce = 0.5f;

    [SerializeField] private float accelrationTime = 0.25f;

    private Rigidbody2D playerRigidbody;
    // Timer used for the lerp for the jump
    private float currentJumpTimer = 0;
    // If the player currently wants to jump
    private bool jump = false;
    private float gravity = 0;
    private float desiredXDir = 0;
    private float acceleration = 0;
    private float extraJump = 0;

    public float CurrentAcceleration { get { return desiredXDir; } }

    private bool isGrounded
    {
        get
        {
            return IsGrounded();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        desiredXDir = Mathf.SmoothDamp(desiredXDir, Input.GetAxisRaw("Horizontal"), ref acceleration, accelrationTime);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            jump = true;
        }

        if (Input.GetKey(KeyCode.Space) && currentJumpTimer > 0)
        {
            currentJumpTimer -= Time.deltaTime * jumpSpeed * 0.5f;
        }

        if (!IsGrounded())
            gravity += gravityScale;
        else if (!jump)
        {
            gravity = 0;
        }
        else
        {
            gravity = 0;
        }

        gravity = Mathf.Clamp(gravity, -maxGravityDownForce, jumpMultiplier);

        if (jump)
        {
            if (currentJumpTimer <= 1)
            {
                currentJumpTimer += Time.deltaTime * jumpSpeed;
            }
            else
            {
                currentJumpTimer = 0;
                jump = false;
            }

            gravity = jumpCurve.Evaluate(currentJumpTimer) * jumpMultiplier;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log($"{IsGrounded()} | {transform.position}");
        playerRigidbody.MovePosition(transform.position + new Vector3(desiredXDir * walkSpeed, gravity));
    }

    private bool IsGrounded()
    {
        RaycastHit2D groundRay = Physics2D.BoxCast(transform.position + new Vector3(0, groundOffset,0 ), new Vector2(1,0.05f),0 , Vector2.down, groundDistance);
        Debug.DrawRay(transform.position + new Vector3(0, groundOffset, 0), Vector2.down);
        if (groundRay.collider != null)
        {
            //Debug.Log(groundRay.collider.name);
            return true;
        }

        return false;
    }
}
