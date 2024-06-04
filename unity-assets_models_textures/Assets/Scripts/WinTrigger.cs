using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Text timerText; // Timer Text
    public Timer timerScript; // Timer Script

    // Called when another collider enters the trigger collider attached to this GameObject
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timerScript.StopTimer();

            // Increase the text size and change the color to green
            timerText.fontSize = 60; // Adjust the font size as needed
            timerText.color = Color.green;
        }
    }
}
