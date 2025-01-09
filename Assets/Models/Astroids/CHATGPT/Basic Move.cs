using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement, adjustable in the inspector

    void Update()
    {
        // Get input for movement along the X and Z axes
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow (X axis)
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down Arrow (Z axis)
        float upDown = 0f;

        // Check for R/F key presses to move along the Y axis (up and down)
        if (Input.GetKey(KeyCode.R)) // Move up (R key)
        {
            upDown = 1f;
        }
        else if (Input.GetKey(KeyCode.F)) // Move down (F key)
        {
            upDown = -1f;
        }

        // Calculate movement vector
        Vector3 movement = new Vector3(horizontal, upDown, vertical) * moveSpeed * Time.deltaTime;

        // Apply the movement to the object's position
        transform.Translate(movement);
    }
}