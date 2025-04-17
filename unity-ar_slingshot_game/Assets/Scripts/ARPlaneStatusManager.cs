using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.UI;

public class ARPlaneStatusManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI planeStatusText; // Text
    [SerializeField] private Image planeStatusBackground; // Background
    [SerializeField] private ARPlaneManager arPlaneManager;

    private void Start()
    {
        // Set initial message and background color
        planeStatusText.text = "Searching for surfaces...";
        planeStatusBackground.color = Color.gray; // Initial color (gray or transparent)
    }

    private void Update()
    {
        // Get the number of detected planes
        int planeCount = arPlaneManager.trackables.count;

        // Check if more than 3 planes are detected
        if (planeCount > 3)
        {
            planeStatusText.text = "SELECT A PLANE";
            planeStatusBackground.color = Color.green;
        }
        else
        {
            planeStatusText.text = "Searching for surfaces...";
            planeStatusBackground.color = Color.gray;
        }
    }

    public void HideStatusMessage()
    {
        planeStatusText.gameObject.SetActive(false);
        planeStatusBackground.gameObject.SetActive(false);
    }
}
