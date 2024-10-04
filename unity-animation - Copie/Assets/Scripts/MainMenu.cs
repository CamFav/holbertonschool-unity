using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Called when the player selects a level
    public void LevelSelect(int level)
    {
        // Save actual scene
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        string sceneName = "Level0" + level;
        
        // Load corresponding scene
        SceneManager.LoadScene(sceneName);
    }

    // Loads the Options scene
    public void OpenOptions()
    {
        // Save actual scene
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        // Load Options scene
        SceneManager.LoadScene("Options");
    }

    // Quits the game
    public void ExitGame()
    {
        Debug.Log("Exited");
        Application.Quit();
    }
}
