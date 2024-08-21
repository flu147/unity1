using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Player transform
    public Vector3 offset;    // Offset for the camera position

    void LateUpdate()
    {
        // Set the camera position to the player's position + offset
        transform.position = target.position + offset;
    }
}
