//Written By AI
using UnityEngine;

public class ObjectSpinner : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("The axis around which the object will spin.")]
    public Vector3 rotationAxis = Vector3.up; // Default to Y-Axis

    [Tooltip("Rotation speed in degrees per second.")]
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotate the object around the specified axis at the given speed
        transform.Rotate(rotationAxis.normalized * rotationSpeed * Time.deltaTime);
    }
}