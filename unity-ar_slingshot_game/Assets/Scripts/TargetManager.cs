using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class TargetManager : MonoBehaviour
{
    public GameObject targetPrefab; // Prefab for targets
    public int targetCount = 5; // Default number of targets

    private ARPlane selectedPlane;
    private List<GameObject> targets = new List<GameObject>(); // List to track targets
    private Camera arCamera;

    private bool targetsInitialized = false; // Keep track of initialized targets

    private void Start()
    {
        arCamera = Camera.main;
    }

    // Call this method when a plane is selected
    public void InitializeTargets(ARPlane plane)
    {
        // If targets have already been initialized, do nothing.
        if (targetsInitialized) return;

        selectedPlane = plane;
        targetsInitialized = true;

        // Create targets on the selected plane
        for (int i = 0; i < targetCount; i++)
        {
            GameObject target = Instantiate(targetPrefab);
            targets.Add(target);

            // Position and initialize target behavior
            PositionTargetOnPlane(target);
            target.AddComponent<TargetBehavior>().Initialize(plane);
        }
    }

    // Position the target on top of the plane
    private void PositionTargetOnPlane(GameObject target)
    {
        if (selectedPlane == null) return;

        // Get random position within plane bounds
        Vector3 randomPosition = GetRandomPointOnPlane();
        target.transform.position = randomPosition;

        // Scale based on distance from camera
        float distance = Vector3.Distance(arCamera.transform.position, randomPosition);
        float scaleFactor = Mathf.Clamp(1 / distance, 0.1f, 1f); // Targets scale
        target.transform.localScale = Vector3.one * scaleFactor;
    }

    // Get a random position in the selected plane
    private Vector3 GetRandomPointOnPlane()
    {
        Vector3 center = selectedPlane.center;
        Vector2 size = selectedPlane.size * 0.5f;

        float randomX = Random.Range(-size.x, size.x);
        float randomZ = Random.Range(-size.y, size.y);

        Vector3 randomPoint = center + new Vector3(randomX, 0, randomZ);
        randomPoint.y = selectedPlane.transform.position.y;

        return randomPoint;
    }
}
