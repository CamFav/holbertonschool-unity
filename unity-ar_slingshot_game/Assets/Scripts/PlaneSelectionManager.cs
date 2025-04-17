using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlaneSelectionManager : MonoBehaviour
{
    private ARPlaneManager arPlaneManager;
    private ARRaycastManager arRaycastManager;

    public static ARPlane selectedPlane = null;

    [SerializeField] private GameObject startButton;

    private GameManager gameManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Start()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Awake()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
        arRaycastManager = GetComponent<ARRaycastManager>();

        if (startButton != null)
            startButton.SetActive(false);
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);

            if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                ARRaycastHit hit = hits[0];
                ARPlane plane = arPlaneManager.GetPlane(hit.trackableId);

                if (plane != null)
                {
                    SelectPlane(plane);
                }
            }
        }
    }

    private void SelectPlane(ARPlane plane)
    {
        selectedPlane = plane;

        foreach (var detectedPlane in arPlaneManager.trackables)
        {
            if (detectedPlane != selectedPlane)
            {
                detectedPlane.gameObject.SetActive(false);
            }
        }

        // Desactive ARPlaneManager when a plane already has been selected
        arPlaneManager.enabled = false;

        // Display Start Button
        if (startButton != null)
            startButton.SetActive(true);

        // Initialize the targets on the selected plane
        FindObjectOfType<TargetManager>().InitializeTargets(selectedPlane);

        FindObjectOfType<GameManager>().OnPlaneSelected();

        // Hide the status message and background image once a plane is selected
        FindObjectOfType<ARPlaneStatusManager>().HideStatusMessage();
    }
}
