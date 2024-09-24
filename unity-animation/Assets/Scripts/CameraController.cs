using UnityEngine;

public class MouseCameraController : MonoBehaviour
{
    public Transform player; // Player reference
    public float rotationSpeed = 5.0f; // Rotation speed of main camera
    public bool isInverted = false;

    private float currentX = 0.0f; 
    private float currentY = 0.0f;
    public float verticalLimit = 80f; // Vertical rotation limit

    void Start()
    {
        isInverted = PlayerPrefs.GetInt("InvertY", 0) == 1;
        Debug.Log("InvertY loaded: " + isInverted);
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        if (isInverted)
        {
            mouseY = -mouseY;
        }

        currentX += mouseX;
        currentY -= mouseY;
        currentY = Mathf.Clamp(currentY, -verticalLimit, verticalLimit);

        // Apply rotation to camera
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.rotation = rotation;

        Vector3 offset = new Vector3(0, 2.5f, -6.25f);
        transform.position = player.position + rotation * offset;

        transform.LookAt(player.position);
    }
}
