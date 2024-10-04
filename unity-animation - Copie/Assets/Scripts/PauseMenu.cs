using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    private bool isPaused = false;

    public AudioSnapshotController audioSnapshotController;

    void Update()
    {
        // If player presses the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();  // Resume game
            }
            else
            {
                Pause();   // Pause the game
            }
        }
    }

    // Pause the game
    public void Pause()
    {
        isPaused = true;
        pauseCanvas.SetActive(true);  // Activate PauseCanvas
        Time.timeScale = 0f;          // Stop game time
        audioSnapshotController.PauseGame(); // Transition to paused snapshot
    }

    // Resume the game
    public void Resume()
    {
        isPaused = false;
        pauseCanvas.SetActive(false);  // Disable PauseCanvas
        Time.timeScale = 1f;           // Resume game time
        audioSnapshotController.ResumeGame(); // Transition to normal audio
    }

    public void Restart()
    {
        Time.timeScale = 1f;  // Reset time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload scene
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;  // Reset time
        // Save the current scene before loading MainMenu
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");  // Load MainMenu Scene
    }

    public void OptionsMenu()
    {
        Time.timeScale = 1f; // Reset time
        // Save the current scene before loading Options
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Options");  // Load Options Scene
    }
}
