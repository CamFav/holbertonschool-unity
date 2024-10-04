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
    /// Reference to the Animator component to control animations on Ty.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Reference to the Ty GameObject where the Animator is located.
    /// </summary>
    public GameObject tyObject;

    /// <summary>
    /// Transform of the player's starting position (spawn point).
    /// </summary>
    public Transform startTransform;

    /// <summary>
    /// The Y-axis threshold below which the player is considered to have fallen off the platform.
    /// </summary>
    public float fallThreshold = -10.0f;

    /// <summary>
    /// Minimum fall speed to trigger the falling animation.
    /// </summary>
    public float fallSpeedThreshold = -3.0f;

    /// <summary>
    /// Variable to track whether the player is currently falling.
    /// </summary>
    private bool isFalling = false;

    /// <summary>
    /// Height above the starting position where the player will respawn if they fall.
    /// </summary>
    public float respawnHeight = 10.0f;

    /// <summary>
    /// Reference to the camera's Transform for determining movement direction based on the camera's orientation.
    /// </summary>
    public Transform cameraTransform;

    /// <summary>
    /// Variables to track the jumping state.
    /// </summary>
    private bool isJumping = false;

    /// <summary>
    /// Called when the script instance is being loaded. Initializes the CharacterController and Animator components.
    /// </summary>
    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Find the Animator component on the Ty GameObject
        animator = tyObject.GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator not found on Ty object.");
        }
    }

    /// <summary>
    /// Called every frame to handle player movement, jumping, and falling logic.
    /// </summary>
    void Update()
    {
        // Get input from horizontal and vertical axis (WASD keys)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Check if the player is grounded
        if (controller.isGrounded)
        {
            // If the player was falling and just landed, trigger Falling Flat Impact
            if (isFalling)
            {
                isFalling = false;
                animator.SetBool("isFalling", false); // Stop falling animation
                animator.SetTrigger("Impact"); // Trigger Falling Flat Impact animation
            }

            // Reset the jump animation if the player has landed after a jump
            if (isJumping)
            {
                isJumping = false;
                animator.SetBool("isJumping", false); // Stop jump animation
            }

            // Calculate the movement direction based on camera orientation and user input
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();

            moveDirection = (cameraForward * moveVertical + cameraRight * moveHorizontal);
            moveDirection *= speed;

            // Rotate the player to face the movement direction
            if (moveDirection != Vector3.zero)
            {
                Vector3 lookDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), 0.15f);
            }

            // Set animator parameter to transition between Idle and Running
            if (moveHorizontal != 0 || moveVertical != 0)
            {
                animator.SetBool("isRunning", true); // Player is moving, set isRunning to true
            }
            else
            {
                animator.SetBool("isRunning", false); // Player stopped, set isRunning to false
            }

            // Check if the player pressed the Jump button
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpForce;
                isJumping = true;
                animator.SetBool("isJumping", true); // Trigger jump animation
            }
        }
        else
        {
            // If the player is falling and the fall speed is below the threshold, trigger the Falling animation
            if (!isJumping && moveDirection.y < fallSpeedThreshold && !isFalling)
            {
                isFalling = true; // Player is falling
                animator.SetBool("isFalling", true); // Trigger the fall animation
            }
        }

        // Apply gravity to the player
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the player using the CharacterController
        controller.Move(moveDirection * Time.deltaTime);

        // Check if the player has fallen below the fall threshold
        if (transform.position.y < fallThreshold)
        {
            // Respawn the player above the starting position
            Vector3 respawnPosition = new Vector3(startTransform.position.x, startTransform.position.y + respawnHeight, startTransform.position.z);
            transform.position = respawnPosition;

            // Reset movement direction
            moveDirection.x = 0;
            moveDirection.z = 0;
        }
        
    }


    public void OnGettingUpEnd()
    {
    Vector3 correctedPosition = new Vector3(transform.position.x, startTransform.position.y, transform.position.z);
    transform.position = correctedPosition;
    }

}
