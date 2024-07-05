using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 5.0f; // Rotation speed of the camera

    private Vector3 offset; // Offset initial between camera and player
    private float currentX = 0.0f;

    void Start()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Rotation based on mouse movements
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;

        // Calcul of the camera rotation
        Quaternion rotation = Quaternion.Euler(0, currentX, 0);

        // Calcul of the new position of the camera after rotation
        Vector3 newPosition = player.position + rotation * offset;

        // Apply new camera position
        transform.position = newPosition;

        // Camera look at player
        transform.LookAt(player.position);

        // Updates the direction of the player following the camera position
        Vector3 direction = Quaternion.Euler(0, currentX, 0) * Vector3.forward;
        player.forward = direction.normalized;
    }
}
