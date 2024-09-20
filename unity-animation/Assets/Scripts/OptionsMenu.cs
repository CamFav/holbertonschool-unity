using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle invertYToggle;

    void Start()
    {
        // Charger l'état d'inversion depuis PlayerPrefs
        bool invertY = PlayerPrefs.GetInt("InvertY", 0) == 1;
        invertYToggle.isOn = invertY;
    }

    // Called when the Apply button is pressed
    public void Apply()
    {
        // Sauvegarder les paramètres
        bool invertY = invertYToggle.isOn;
        PlayerPrefs.SetInt("InvertY", invertY ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log("InvertY saved: " + PlayerPrefs.GetInt("InvertY"));
        // Retourner à la scène précédente
        SceneManager.LoadScene(PlayerPrefs.GetString("PreviousScene", "MainMenu"));
    }

    // Called when the Back button is pressed
    public void Back()
    {
        // Retourner à la scène précédente sans sauvegarder les modifications
        SceneManager.LoadScene(PlayerPrefs.GetString("PreviousScene", "MainMenu"));
    }
}
