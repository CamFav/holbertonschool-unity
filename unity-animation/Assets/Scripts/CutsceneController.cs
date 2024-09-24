using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject timerCanvas;

    public GameObject mainCamera;

    private void Start()
    {

        // Unable player movements
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Start animation
        Animator animator = GetComponent<Animator>();
        animator.Play("Intro01");
    }

    public void OnAnimationEnd()
    {
        // Activate main camera at the end of the animation
        if (mainCamera != null)
        {
            mainCamera.SetActive(true);
        }
        else
        {
            Debug.LogError("Main camera not found");
        }

        // Activate PlayerController
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = true; // Enable PlayerMovement at the end of the animation
        }
        else
        {
            Debug.LogError("PlayerController not found");
        }

        // Activate TimerCanvas
        if (timerCanvas != null)
        {
            timerCanvas.SetActive(true); // Activate TimerCanvas at the end
        }
        else
        {
            Debug.LogError("TimerCanvas reference not set");
        }

        gameObject.SetActive(false);
    }
}
