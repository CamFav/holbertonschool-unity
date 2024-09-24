using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Speed of the player's movement.
    /// </summary>
    public float speed = 10.0f;

    /// <summary>
    /// Force applied when the player jumps.
    /// </summary>
    public float jumpForce = 8.0f;

    /// <summary>
    /// Gravity applied to the player.
    /// </summary>
    public float gravity = 20.0f;

    /// <summary>
    /// The movement direction of the player.
    /// </summary>
    private Vector3 moveDirection = Vector3.zero;

    /// <summary>
    /// Reference to the CharacterController component.
    /// </summary>
    private CharacterController controller;

    /// <summary>
    /// Transform of the player's starting position (spawn point).
    /// </summary>
    public Transform startTransform; 

    /// <summary>
    /// The Y-axis threshold below which the player is considered to have fallen off the platform.
    /// </summary>
    public float fallThreshold = -10.0f; 

    /// <summary>
    /// Height above the starting position where the player will respawn if they fall.
    /// </summary>
    public float respawnHeight = 10.0f;

    /// <summary>
    /// Reference to the camera's Transform for determining movement direction based on the camera's orientation.
    /// </summary>
    public Transform cameraTransform;

    /// <summary>
    /// Called when the script instance is being loaded. Initializes the CharacterController component.
    /// </summary>
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Called every frame to handle player movement, jumping, and falling logic.
    /// </summary>
    void Update()
    {
        // Check if the player is grounded
        if (controller.isGrounded)
        {
            // Get input from horizontal and vertical axis (WASD keys)
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Get the forward and right vectors relative to the camera's orientation
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            // Flatten the vectors to remove vertical component and normalize them
            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();

            /// <summary>
            /// Calculate the movement direction based on camera orientation and user input.
            /// </summary>
            moveDirection = (cameraForward * moveVertical + cameraRight * moveHorizontal);
            moveDirection *= speed;

            // Rotate the player to face the movement direction
            if (moveDirection != Vector3.zero)
            {
                Vector3 lookDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), 0.15f);
            }

            // Check if the player pressed the Jump button
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        // Apply gravity to the player
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the player using the CharacterController
        controller.Move(moveDirection * Time.deltaTime);

        // Check if the player has fallen below the fall threshold
        if (transform.position.y < fallThreshold)
        {
            /// <summary>
            /// Respawn the player above the starting position to simulate falling from the air.
            /// Reset horizontal movement to ensure the player falls straight down.
            /// </summary>
            Vector3 respawnPosition = new Vector3(startTransform.position.x, startTransform.position.y + respawnHeight, startTransform.position.z);
            transform.position = respawnPosition;

            // Reset the horizontal movement direction to zero (no movement in x or z)
            moveDirection.x = 0;
            moveDirection.z = 0;
        }
    }
}
