using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private const float AXIS_MAX = 1;
    private const float AXIS_MIN = -1;

    private Rigidbody rb;
    private float throttleInput;
    [SerializeField][Range(AXIS_MIN,AXIS_MAX)] private float thrust;
    [SerializeField] private float thrustStrength = 50f;
    private bool thrustBuffer = false;
    // [SerializeField] private Vector3 velocity = new(0, 0, 0);
    // [SerializeField] private float drag = 1f;
    [SerializeField] private float translationStrength = 5f;
    private Vector3 translationalInput = new(0, 0, 0);
    private Vector3 translationalVelocity = new(0,0,0);
    // [SerializeField] private float translationalDrag = 4f;
    [SerializeField] private Vector3 rotationalInput = new(0,0,0);
    [SerializeField] private Vector2 primaryRotationSensitivity = new(0.001f,0.001f);
    [SerializeField] private Vector3 rotationStrength = new(100, 100, 100);
    [Tooltip("How many seconds it takes for a binary input axis to change its input from min to max")]
    [SerializeField] private float binaryAxisSmoother = 0.4f;
    private Vector3 rotationalOutput;
    public float Thrust => thrust;
    public Vector3 RotationalInput => rotationalOutput;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        float newThrust = thrust;
        newThrust += throttleInput * Time.deltaTime;
        if (Mathf.Sign(thrust) != Mathf.Sign(newThrust) && thrust != 0f)
        {
            thrust = 0f;
            thrustBuffer = true;
        }
        if (thrustBuffer && throttleInput == 0) thrustBuffer = false;
        if (!thrustBuffer) thrust = newThrust;
        
        translationalVelocity += translationalInput * Time.deltaTime;
        
        thrust = Mathf.Clamp(thrust, AXIS_MIN, AXIS_MAX);
        translationalVelocity.Clamp(AXIS_MIN, AXIS_MAX);
        
        rotationalOutput = new Vector3(
            rotationalInput.x,
            Mathf.Lerp(rotationalOutput.y, rotationalInput.y, Mathf.Clamp(Time.deltaTime / binaryAxisSmoother, -1, 1)),
            rotationalInput.z);
        
        //transform.Rotate(Vector3.Scale(rotation, rotationStrength) * Time.deltaTime);
        rb.angularVelocity = transform.rotation * Vector3.Scale(rotationalOutput, rotationStrength);
        //rb.AddTorque(transform.rotation * Vector3.Scale(rotation, rotationStrength));
        
        //velocity += transform.forward * (Time.deltaTime * thrust * thrustStrength);
        //velocity += transform.rotation * translationalVelocity * (translationStrength * Time.deltaTime);
        //transform.position += velocity * Time.deltaTime;
        rb.AddForce(transform.forward * (Time.deltaTime * thrust * thrustStrength));
        rb.AddForce(transform.rotation * translationalVelocity * (translationStrength * Time.deltaTime));
        
        //translationalVelocity *= 1 - translationalDrag * Mathf.Min(Time.deltaTime,1);
        //velocity *= 1 - drag * Mathf.Min(Time.deltaTime,1);

        //if (velocity.magnitude < 0.0001f) velocity = new Vector3(0,0,0);
    }
    public void OnTranslate(InputAction.CallbackContext context) =>
        translationalInput = context.ReadValue<Vector3>();
    public void OnThrottle(InputAction.CallbackContext context) =>
        throttleInput = context.ReadValue<float>();

    public void OnPrimaryRotation(InputAction.CallbackContext context)
    {
        Vector3 a = rotationalInput;
        Vector2 b = context.ReadValue<Vector2>();
        Vector2 c = primaryRotationSensitivity;
        
        a.z = Mathf.Max(Mathf.Min(a.z -= b.x * c.x, AXIS_MAX), AXIS_MIN);
        a.x = Mathf.Max(Mathf.Min(a.x -= b.y * c.y, AXIS_MAX), AXIS_MIN);

        rotationalInput = a;
    }
    public void OnYaw(InputAction.CallbackContext context) => 
        rotationalInput.y = context.ReadValue<float>();
}
