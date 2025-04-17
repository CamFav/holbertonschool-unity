using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TargetBehavior : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private ARPlane plane;

    public void Initialize(ARPlane assignedPlane)
    {
        plane = assignedPlane;
        SetRandomDirection();
        speed = Random.Range(0.1f, 0.5f); // Speedd range
    }

    private void Update()
    {
        // Move the target in a random direction
        transform.Translate(direction * speed * Time.deltaTime);

        // Keep the target within plane bounds
        KeepTargetWithinBounds();
    }

    private void SetRandomDirection()
    {
        // Generate a random direction on the X-Z in plane
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
    }

    private void KeepTargetWithinBounds()
    {
        Vector3 localPos = plane.transform.InverseTransformPoint(transform.position);

        Vector2 halfSize = plane.size * 0.5f;
        localPos.x = Mathf.Clamp(localPos.x, -halfSize.x, halfSize.x);
        localPos.z = Mathf.Clamp(localPos.z, -halfSize.y, halfSize.y);

        transform.position = plane.transform.TransformPoint(localPos);
    }
}
