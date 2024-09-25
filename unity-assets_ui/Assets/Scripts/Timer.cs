using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public Text TimerText;  // TimerText Component
    private float elapsedTime;
    private bool isRunning = false;
    public TextMeshProUGUI FinalTimeText;

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    // Resumes the timer
    public void ResumeTimer()
    {
        isRunning = true;
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);  // Minutes
        float seconds = elapsedTime % 60F;  // Secondes
        TimerText.text = string.Format("{0:0}:{1:00.00}", minutes, seconds); // 
    }

    // Method to be called when player wins
    public void Win()
    {
        StopTimer(); // Ensure the timer is stopped
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);  // Minutes
        float seconds = elapsedTime % 60F;  // Seconds
        if (FinalTimeText != null)
        {
            FinalTimeText.text = string.Format("{0:0}:{1:00.00}", minutes, seconds);
        }
    }
}
