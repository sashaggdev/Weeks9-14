using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    // How smoothly the camera follows
    public float smoothSpeed = 0.125f;

    // Camera offset
    public Vector3 offset = new Vector3(3f, 1f, -10f);

    void FixedUpdate()
    {
        if (target == null) return;

        // Camera position
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move towards that position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
