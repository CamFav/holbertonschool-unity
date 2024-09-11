using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Called when the player selects a level
    public void LevelSelect(int level)
    {
        // Sauvegarder la scène actuelle
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        // Construire le nom de la scène en fonction du niveau sélectionné
        string sceneName = "Level0" + level;
        
        // Charger la scène avec le nom correspondant
        SceneManager.LoadScene(sceneName);
    }

    // Loads the Options scene
    public void OpenOptions()
    {
        // Sauvegarder la scène actuelle
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        // Charger la scène des options
        SceneManager.LoadScene("Options");
    }

    // Quits the game
    public void ExitGame()
    {
        Debug.Log("Exited");
        Application.Quit();
    }
}
