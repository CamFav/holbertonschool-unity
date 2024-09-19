using UnityEngine;
using Vuforia;

public class TrackableCanvasController : MonoBehaviour
{
    public GameObject canvas;

    private ObserverBehaviour observerBehaviour;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();

        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
        else
        {
            Debug.LogError("ObserverBehaviour component is missing!");
        }

        // Check if canvas is already desactivated
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour observerBehaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED && status.StatusInfo == StatusInfo.NORMAL)
        {
            // Display Canvas when ImageTarget is detected
            if (canvas != null)
            {
                canvas.SetActive(true);
            }
        }
        else
        {
            // Hide canvas when ImageTarget is lost
            if (canvas != null)
            {
                canvas.SetActive(false);
            }
        }
    }

    void OnDestroy()
    {
        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }
}
