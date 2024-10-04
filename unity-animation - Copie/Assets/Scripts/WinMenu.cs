using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    public void MainMenu()
    {
        // Load the Main Menu Scene
        SceneManager.LoadScene("MainMenu");
    }

    public void Next()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        // Check if there is a next level
        if (currentSceneIndex + 1 < totalScenes)
        {
            // Load the next level
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
