using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 5.0f; // Speed of the camera rotation
    public bool isInverted = false; // Invert the Y-axis

    private Vector3 offset;
    private float currentX = 0.0f; // Rotation around the Y-axis
    private float currentY = 0.0f; // Rotation around the X-axis
    public float verticalLimit = 80f; // Limit for vertical rotation

    void Start()
    {
        // Charger l'état d'inversion depuis PlayerPrefs
        isInverted = PlayerPrefs.GetInt("InvertY", 0) == 1;
        Debug.Log("InvertY loaded: " + isInverted);
        offset = transform.position - player.position;
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

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 newPosition = player.position + rotation * offset;
        transform.position = newPosition;
        transform.LookAt(player.position);

        Vector3 direction = Quaternion.Euler(0, currentX, 0) * Vector3.forward;
        player.forward = direction.normalized;
    }
}
