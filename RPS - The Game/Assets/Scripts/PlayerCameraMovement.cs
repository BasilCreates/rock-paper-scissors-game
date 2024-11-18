using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensitivity of the mouse
    public Transform playerBody; // Reference to the player body or parent object to rotate

    float xRotation = 0f; // Tracks the up/down (pitch) rotation

    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust xRotation (looking up/down) and clamp it to prevent flipping over
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Restrict vertical rotation to 90 degrees

        // Rotate the camera up/down based on mouseY
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player body left/right based on mouseX
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

