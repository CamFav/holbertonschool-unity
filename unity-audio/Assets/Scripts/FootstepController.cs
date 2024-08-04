using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public AudioClip grassFootstep;
    public AudioClip rockFootstep;
    private AudioSource audioSource;
    private CharacterController characterController;
    private bool isRunning;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is grounded and moving
        if (characterController.isGrounded && characterController.velocity.magnitude > 0.1f)
        {
            if (!isRunning)
            {
                isRunning = true;
                InvokeRepeating("PlayFootstepSound", 0, 0.5f);
            }
        }
        else
        {
            isRunning = false;
            CancelInvoke("PlayFootstepSound");
        }
    }

    void PlayFootstepSound()
    {
        // Determine the type of surface
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
        {
            if (hit.collider.CompareTag("Grass"))
            {
                audioSource.clip = grassFootstep;
            }
            else if (hit.collider.CompareTag("Rock"))
            {
                audioSource.clip = rockFootstep;
            }
        }

        // footstep sound
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
