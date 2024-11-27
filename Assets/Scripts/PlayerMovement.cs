using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// BooleanInputs
public enum BoIn
{
    PRIMARY,
    SECONDARY,
    YAW_RIGHT,
    YAW_LEFT,
    THROTTLE_UP,
    THROTTLE_DOWN
}
public class PlayerMovement : MonoBehaviour
{
    [SerializeField][Range(0,1)] public float Thrust; // { get; private set; }
    [SerializeField] public float MaxThrust; // { get; private set; }
    [SerializeField] public Vector3 Velocity; // { get; private set; }
    [SerializeField] private float drag = 1;
    [SerializeField] private float translationStrength = 2;
    [SerializeField] private Vector3 translationVelocity = new();

    private Dictionary<BoIn, bool> inputs = new Dictionary<BoIn, bool>()
    {
        [BoIn.PRIMARY] = false,
        [BoIn.SECONDARY] = false,
        [BoIn.YAW_RIGHT] = false,
        [BoIn.YAW_LEFT] = false,
        [BoIn.THROTTLE_UP] = false,
        [BoIn.THROTTLE_DOWN] = false
    };
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (inputs[BoIn.THROTTLE_UP]) Thrust += Time.deltaTime;
        if (inputs[BoIn.THROTTLE_DOWN]) Thrust -= Time.deltaTime;

        Thrust %= 2;
        
        Velocity += transform.forward * Time.deltaTime * Thrust;
        transform.position += Velocity * Time.deltaTime;
        transform.position += translationVelocity * Time.deltaTime;

        Velocity *= 1 - drag * Mathf.Min(Time.deltaTime,1);

        if (Velocity.magnitude < 0.0001f) Velocity = new Vector3(0,0,0);

        for (int i = 0; i < inputs.Count; i++)
        {
            inputs[(BoIn)i] = false;
        }
    }

    public void OnTranslate(InputAction.CallbackContext context)
    {
        translationVelocity = context.ReadValue<Vector3>();
    }
    public void OnPrimary() => inputs[BoIn.PRIMARY] = true;
    public void OnSecond() => inputs[BoIn.SECONDARY] = true;
    public void OnYawRight() => inputs[BoIn.YAW_RIGHT] = true;
    public void OnYawLeft() => inputs[BoIn.YAW_LEFT] = true;
    public void OnThrottleUp() => inputs[BoIn.THROTTLE_UP] = true;
    public void OnThrottleDown() => inputs[BoIn.THROTTLE_DOWN] = true;
}
