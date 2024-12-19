using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float jumpForce = 5f; // Force applied when jumping
    public float fallMultiplier = 5f;
    public float lookSpeedX = 2f; // Rotation speed for the mouse X axis
    public Rigidbody rb; // Reference to the Rigidbody component
    public LayerMask groundMask; // LayerMask to define what is considered ground

    public bool isGrounded; // Check if the player is on the ground

    private void Start()
    {
        // Get the Rigidbody component if not assigned
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        // Lock and hide the cursor for a better experience
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Mouse look functionality (left and right)
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        transform.Rotate(0, mouseX, 0);

        // Create a movement vector
        Vector3 moveDirection = Vector3.zero;

        // Check individual key presses for movement
        if (Input.GetKey(KeyCode.W)) // Move forward
        {
            moveDirection += transform.forward; // Forward direction
        }
        if (Input.GetKey(KeyCode.S)) // Move backward
        {
            moveDirection += -transform.forward; // Backward direction
        }
        if (Input.GetKey(KeyCode.A)) // Move left
        {
            moveDirection += -transform.right; // Left direction
        }
        if (Input.GetKey(KeyCode.D)) // Move right
        {
            moveDirection += transform.right; // Right direction
        }

        // Normalize the direction to maintain consistent speed
        moveDirection.Normalize();

        // Move the player using Rigidbody
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

        // Check for jumping
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) // Check if grounded and space is pressed
        {
            Jump();
        }
        // Handle faster falling
        if (rb.velocity.y < 0) // When player is falling
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        // Check if the player is grounded. A physics,checksphere means it makes an invisible sphere that checks the radius around the object.
        // it checks if anything intersects inside the sphere like a groundmask, then makes the bool true or false depending.
        isGrounded = Physics.CheckSphere(transform.position, 50f, groundMask);
    }

    void Jump()
    {
        if (isGrounded)
        {
            transform.position += new Vector3(0, jumpForce, 0); // manually adds height to the character after jumping
        }
    }
}
