using UnityEngine;

public class LandingSoundController : MonoBehaviour
{
    public AudioClip grassLanding;
    public AudioClip rockLanding;
    private AudioSource audioSource;
    private CharacterController characterController;
    private bool isFalling;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if player is falling
        if (!characterController.isGrounded && characterController.velocity.y < 0)
        {
            isFalling = true;
        }
        
        // Check if player has landed
        if (isFalling && characterController.isGrounded)
        {
            PlayLandingSound();
            isFalling = false;
        }
    }

    void PlayLandingSound()
    {
        // Determine the type of surface
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
        {
            if (hit.collider.CompareTag("Grass"))
            {
                audioSource.clip = grassLanding;
            }
            else if (hit.collider.CompareTag("Rock"))
            {
                audioSource.clip = rockLanding;
            }
        }

        // Play landing sound
        audioSource.PlayOneShot(audioSource.clip);
    }
}
