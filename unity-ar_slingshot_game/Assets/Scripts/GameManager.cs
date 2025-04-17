using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject ammoPrefab;

    private bool planeSelected = false;

    private void Start()
    {
        startButton.SetActive(false); // Hide the Start button initially
    }

    public void OnPlaneSelected()
    {
        // method is called when a plane is selected
        planeSelected = true;
        startButton.SetActive(true); // show the start button
    }

    public void OnStartButtonPressed()
    {
        if (planeSelected)
        {
            // Instantiate ammo at the center of the screen
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 1f);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenCenter);
            Instantiate(ammoPrefab, worldPosition, Quaternion.identity);

            // Hide the start button after starting the game
            startButton.SetActive(false);
        }
    }
}
