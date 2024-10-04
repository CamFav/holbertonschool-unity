using UnityEngine;

public class FootstepAndLandingSoundController : MonoBehaviour
{
    public AudioSource footstepAudioSource; // Separate AudioSource for footsteps
    public AudioSource landingAudioSource;  // Separate AudioSource for landing sounds
    public AudioClip grassFootstepClip;
    public AudioClip rockFootstepClip;
    public AudioClip landingGrassClip;
    public AudioClip landingRockClip;

    private CharacterController characterController;
    private Animator animator;

    private bool isRunning;
    private bool isOnGrass;
    private bool isOnStone;
    private bool isJumping;
    private bool isFalling;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Check if player is moving and grounded
        isRunning = characterController.velocity.magnitude > 0.1f && characterController.isGrounded;

        // Check from animation if state == jumping or falling
        isJumping = animator.GetCurrentAnimatorStateInfo(0).IsName("Jump");
        isFalling = animator.GetCurrentAnimatorStateInfo(0).IsName("Fall");

        // Play walking sound if the player is not falling or jumping
        if (isRunning && !isJumping && !isFalling && !footstepAudioSource.isPlaying)
        {
            PlayFootstepSound();
        }
        else if (!isRunning && footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Stop();
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if player is on Grass or Stone platforms
        if (hit.collider.CompareTag("Grass"))
        {
            isOnGrass = true;
            isOnStone = false;
        }
        else if (hit.collider.CompareTag("Stone"))
        {
            isOnGrass = false;
            isOnStone = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.y > 0.5f)
        {
            if (collision.collider.CompareTag("Grass"))
            {
                PlayLandingSound(landingGrassClip);
            }
            else if (collision.collider.CompareTag("Stone"))
            {
                PlayLandingSound(landingRockClip);
            }
        }
    }

    private void PlayFootstepSound()
    {
        if (isOnGrass)
        {
            footstepAudioSource.clip = grassFootstepClip;
        }
        else if (isOnStone)
        {
            footstepAudioSource.clip = rockFootstepClip;
        }

        footstepAudioSource.loop = true;
        footstepAudioSource.Play();
    }

    private void PlayLandingSound(AudioClip landingClip)
    {
        landingAudioSource.PlayOneShot(landingClip); // Play landing sound without stopping footsteps
    }
}
