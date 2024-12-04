using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private const float axisMax = 1;
    private const float axisMin = -1;
    private float throttleAcceleration;
    [SerializeField][Range(axisMin,axisMax)] private float thrust;
    [SerializeField] private float thrustStrength = 50f;
    [SerializeField] private Vector3 velocity = new(0, 0, 0);
    [SerializeField] private float drag = 1f;
    [SerializeField] private float translationStrength = 5f;
    private Vector3 translationalAcceleration = new(0, 0, 0);
    private Vector3 translationalVelocity = new(0,0,0);
    [SerializeField] private float translationalDrag = 4f;
    [SerializeField] private Vector3 rotation = new(0,0,0);
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
        thrust += throttleAcceleration * Time.deltaTime;
        translationalVelocity += translationalAcceleration * Time.deltaTime;
        
        thrust = Mathf.Clamp(thrust, axisMin, axisMax);
        translationalVelocity.Clamp(axisMin, axisMax);
        
        transform.Rotate(Vector3.Scale(rotation, rotationStrength) * Time.deltaTime);
        
        velocity += transform.forward * (Time.deltaTime * thrust * thrustStrength);
        velocity += transform.rotation * translationalVelocity * (translationStrength * Time.deltaTime);
        transform.position += velocity * Time.deltaTime;

        translationalVelocity *= 1 - translationalDrag * Mathf.Min(Time.deltaTime,1);
        velocity *= 1 - drag * Mathf.Min(Time.deltaTime,1);

        if (velocity.magnitude < 0.0001f) velocity = new Vector3(0,0,0);
    }
    public void OnTranslate(InputAction.CallbackContext context) =>
        translationalAcceleration = context.ReadValue<Vector3>();
    public void OnThrottle(InputAction.CallbackContext context) =>
        throttleAcceleration = context.ReadValue<float>();

    public void OnPrimaryRotation(InputAction.CallbackContext context)
    {
        Vector3 a = rotation;
        Vector2 b = context.ReadValue<Vector2>();
        Vector2 c = primaryRotationSensitivity;
        
        a.z = Mathf.Max(Mathf.Min(a.z -= b.x * c.x, axisMax), axisMin);
        a.x = Mathf.Max(Mathf.Min(a.x -= b.y * c.y, axisMax), axisMin);

        rotation = a;
    }
    public void OnYaw(InputAction.CallbackContext context) => 
        rotation.y = context.ReadValue<float>();
}
