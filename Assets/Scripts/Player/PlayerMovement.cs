using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] 
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public int numberOfJumps;
    public int maxNumberOfJumps;
    public float airMultiplier;
    private bool readyToJump;
    
    [Header("Keybinds")] 
    public KeyCode jumpKey = KeyCode.Space;
    
    [Header("Ground Check")] 
    public float playerHeight;
    public LayerMask groundLayer;
    
    private bool grounded;
    
    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;
    
    private Vector3 moveDirection;

    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        
        rigidBody.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
        
        PlayerInput();
        
        SpeedControl();
        
        // Handle drag
        if (grounded)
        {
            rigidBody.drag = groundDrag;
            
            numberOfJumps = 0;
        }
        else
        {
            rigidBody.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // When to jump
        if (Input.GetKeyDown(jumpKey))
        {
            if (readyToJump && grounded)
            {
                readyToJump = false;

                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }

            if (readyToJump && !grounded)
            {
                readyToJump = false;

                Jump();
            }
        }
    }

    private void MovePlayer()
    {
        // Calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        // On ground
        if (grounded)
        {
            rigidBody.AddForce(moveDirection.normalized * (moveSpeed * 10.0f), ForceMode.Force);
            
            readyToJump = true;
        }
        else if (!grounded)
        {
            rigidBody.AddForce(moveDirection.normalized * (moveSpeed * 10.0f * airMultiplier), ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rigidBody.velocity.x, 0.0f, rigidBody.velocity.z);
        
        // Limit velocity if needed
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rigidBody.velocity = new Vector3(limitedVelocity.x, rigidBody.velocity.y, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        // Reset y velocity
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0.0f, rigidBody.velocity.z);
        
        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;

        numberOfJumps++;

        if (numberOfJumps >= maxNumberOfJumps)
        {
            readyToJump = false;
            
            numberOfJumps = 0;
        }
    }
}
