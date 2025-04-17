using UnityEngine;

public class AmmoBehavior : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 dragStart;
    private Vector3 dragEnd;
    private bool isDragging = false; // Flag for dragging state
    private Rigidbody rb;

    [SerializeField] private float launchForceMultiplier = 10f; // Launch force

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ResetAmmo(); // Position the Ammo at the start
    }

    private void Update()
    {
        if (isDragging)
        {
            // Update the drag position while player is dragging
            Vector3 touchPosition = GetTouchPosition();
            transform.position = touchPosition;
        }
    }

    private Vector3 GetTouchPosition()
    {
        // Convert screen touch position to world position on the plane
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(screenCenter.x, screenCenter.y, 1f)); // Distance from camera
        return touchPos;
    }

    private void OnMouseDown()
    {
        // Begin dragging
        isDragging = true;
        dragStart = transform.position;
        rb.isKinematic = true; // Stop physics during drag
    }

    private void OnMouseUp()
    {
        // End dragging and launch
        isDragging = false;
        dragEnd = transform.position;

        // Calculate launch direction and force
        Vector3 launchDirection = (dragStart - dragEnd).normalized;
        float dragDistance = Vector3.Distance(dragStart, dragEnd);
        Vector3 launchForce = launchDirection * dragDistance * launchForceMultiplier;

        // Apply launch force
        rb.isKinematic = false; // Enable physics for launch
        rb.AddForce(launchForce, ForceMode.Impulse);
    }

    private void ResetAmmo()
    {
        // Reset the position and physics of Ammo
        startPosition = GetTouchPosition(); // Set to center of the screen
        transform.position = startPosition;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Reset Ammo if (hits a target) or (hits the plane) or (falls off the plane)
        if (collision.gameObject.CompareTag("Target") || collision.gameObject.CompareTag("Plane"))
        {
            ResetAmmo();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset Ammo if it falls outside the plane
        if (other.CompareTag("Plane"))
        {
            ResetAmmo();
        }
    }
}
