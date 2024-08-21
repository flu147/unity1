using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 700f;  // Speed of player rotation
    public Rigidbody rb;

    private Vector3 moveDirection;

    void Update()
    {
        // Get input from WASD or arrow keys
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // Set movement direction
        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        // Rotate the player to face the cursor
        RotateTowardsCursor();
    }

    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void RotateTowardsCursor()
    {
        // Raycast from the camera to the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);
            Vector3 directionToTarget = (targetPoint - transform.position).normalized;
            directionToTarget.y = 0;  // Keep the rotation on the Y-axis only

            // Calculate the rotation
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // Smoothly rotate towards the target rotation
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime));
        }
    }
}
