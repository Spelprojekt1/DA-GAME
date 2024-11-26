using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField][Range(0,1)] public float Thrust; // { get; private set; }
    [SerializeField] public float MaxThrust; // { get; private set; }
    [SerializeField] public Vector3 Velocity; // { get; private set; }

    [SerializeField] private float drag = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Velocity += transform.forward * Time.deltaTime * Thrust;
        transform.position += Velocity * Time.deltaTime;

        Velocity *= 1 - drag * Time.deltaTime;

        if (Velocity.magnitude < 0.0001f) Velocity = new Vector3(0,0,0);
    }
}
