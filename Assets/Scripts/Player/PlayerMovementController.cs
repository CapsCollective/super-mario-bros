using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [Space]

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

    private SpriteRenderer spriteRenderer;
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

    // Different powers, used as an argument for PowerUp() so other entities can power-up Mario
    public enum Power { None, Flower, Mushroom, Star }

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
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        desiredXDir = Mathf.SmoothDamp(desiredXDir, Input.GetAxisRaw("Horizontal"), ref acceleration, accelrationTime);
        animator.SetFloat("speed", Mathf.Abs(desiredXDir));
        animator.SetBool("isJumping", !isGrounded);

        spriteRenderer.flipX = Mathf.Sign(desiredXDir) < 0 ? true : false;

        if (IsColliding())
            desiredXDir = 0;

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
        playerRigidbody.MovePosition(transform.position + new Vector3(desiredXDir * walkSpeed, gravity));
        //Debug.Log($"{IsGrounded()} | {transform.position}");'
    }
    
    private bool IsColliding()
    {
        RaycastHit2D collisionCheck = Physics2D.BoxCast(transform.position + new Vector3(Mathf.Sign(desiredXDir) * 0.55f, 0, 0), new Vector2(0.05f, .9f), 0, Vector3.right * Mathf.Sign(desiredXDir), .05f);
        if (collisionCheck.collider != null)
        {
            Debug.Log(collisionCheck.collider.name);
            return true;
        }
        return false;
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

    public void PowerUp(Power power)
    {
        switch (power)
        {
            case Power.Flower:
                Debug.Log("Player received Flower Power!");
                break;
            case Power.Mushroom:
                Debug.Log("Player received Mushroom Power!");
                break;
            case Power.Star:
                Debug.Log("Player received Star Power!");
                break;
            default:
                Debug.Log("Player didn't recieve a Power!");
                break;
        }
    }

    public void AddLives(int numLives)
    {
        // Increase lives by numLives
        Debug.Log("Recieved " + numLives + " lives (not functional yet)!");
    }

    public void Die()
    {
        // Decrement player lives and respawn etc...
        Debug.Log("Player has died (not functional yet)!");
    }
}
