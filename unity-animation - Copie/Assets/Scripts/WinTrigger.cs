using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Canvas winCanvas;
    public Timer timerScript;

    // Called when another collider enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Stop the timer
            timerScript.StopTimer();

            // Activate the WinCanvas
            if (winCanvas != null)
            {
                winCanvas.gameObject.SetActive(true);
            }

            // Optional:
            // timerText.fontSize = 60; // Adjust the font size as needed
            // timerText.color = Color.green;

            // Call Win method in Timer script to update FinalTime
            timerScript.Win();
        }
    }
}
