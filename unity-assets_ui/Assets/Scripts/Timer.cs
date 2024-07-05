using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimerText;  // TimerText Component
    private float elapsedTime;
    private bool isRunning = false;

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

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);  // Minutes
        float seconds = elapsedTime % 60F;  // Secondes
        TimerText.text = string.Format("{0:0}:{1:00.00}", minutes, seconds); // 
    }
}
