using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField][Range(0,1)] private float thrust; // { get; private set; }
    [SerializeField] private float maxThrust = 1f; // { get; private set; }
    [SerializeField] private float thrustStrength = 50f;
    [SerializeField] private Vector3 velocity /*{ get; private set; }*/ = new(0, 0, 0);
    [SerializeField] private float drag = 1f;
    [SerializeField] private float translationStrength = 5f;
    [SerializeField] private Vector3 translationVelocity = new(0,0,0);
    [SerializeField] private float jerk = 0f;
    [SerializeField] private Vector3 rotation /*{ get; private set; }*/ = new(0,0,0);
    [SerializeField] private Vector2 primaryRotationSensitivity = new(0.001f,0.001f);
    [SerializeField] private Vector3 rotationStrength = new(100, 100, 100);

    public float Thrust => thrust;
    public Vector3 Rotation => rotation;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        thrust += jerk * Time.deltaTime;

        if (thrust > 1) thrust = 1;
        if (thrust < -1) thrust = -1;
        
        transform.Rotate(Vector3.Scale(rotation, rotationStrength) * Time.deltaTime);
        
        velocity += transform.forward * (Time.deltaTime * thrust * thrustStrength);
        transform.position += velocity * Time.deltaTime;
        transform.position += translationVelocity * (translationStrength * Time.deltaTime);

        velocity *= 1 - drag * Mathf.Min(Time.deltaTime,1);

        if (velocity.magnitude < 0.0001f) velocity = new Vector3(0,0,0);
    }
    public void OnTranslate(InputAction.CallbackContext context) =>
        translationVelocity = context.ReadValue<Vector3>();
    public void OnThrottle(InputAction.CallbackContext context) =>
        jerk = context.ReadValue<float>();

    public void OnPrimaryRotation(InputAction.CallbackContext context)
    {
        Vector3 a = rotation;
        Vector2 b = context.ReadValue<Vector2>();
        Vector2 c = primaryRotationSensitivity;
        
        a.z = Mathf.Max(Mathf.Min(a.z -= b.x * c.x, 1), -1);
        a.x = Mathf.Max(Mathf.Min(a.x -= b.y * c.y, 1), -1);

        rotation = a;
    }
    public void OnYaw(InputAction.CallbackContext context) => 
        rotation.y = context.ReadValue<float>();
    public void OnPrimary() => Debug.Log("Primary");
    public void OnSecondary() => Debug.Log("Secondary");
}
