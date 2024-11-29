using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Homing : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;
    // Target to home in on
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desired = (target.position - transform.position).normalized * speed;
        Vector3 steering = (desired - velocity).normalized * rotationSpeed;
        velocity += steering * Time.deltaTime;

        transform.Translate(velocity * Time.deltaTime);
    }
}
