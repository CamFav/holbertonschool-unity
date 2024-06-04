using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Material trapMat;
    public Material goalMat;
    public Toggle colorblindMode;

    // Method to load the Maze scene with or not ColorBlind Mode
    public void PlayMaze()
    {
        if (colorblindMode.isOn)
        {
            trapMat.color = new Color32(255, 112, 0, 255); // Orange
            goalMat.color = Color.blue;
        }
        else
        {
            trapMat.color = Color.red; // Original red color
            goalMat.color = Color.green; // Original green color
        }

        SceneManager.LoadScene("maze");
    }

    // Method to close the game window
    public void QuitMaze(){
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
