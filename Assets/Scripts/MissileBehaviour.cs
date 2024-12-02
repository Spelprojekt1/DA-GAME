using UnityEngine;
using UnityEngine.Serialization;

public class MissileBehaviour : MonoBehaviour
{
    // [SerializeField] private Vector3 velocity;
    [FormerlySerializedAs("speed")] [SerializeField] private float force = 25f;
    [FormerlySerializedAs("rotationForce")] [FormerlySerializedAs("rotationSpeed")] [SerializeField] private float torque = 50f;
    // Target to home in on
    public Transform target;
    public float lifeTime = 5f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // velocity = transform.forward * force;
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
            return;
        }

        lifeTime -= Time.deltaTime;
        
        Vector3 desired = (target.position - transform.position).normalized;
        // Vector3 steering = (desired - velocity).normalized * rotationSpeed;
        // velocity += steering * Time.deltaTime;
        //transform.Translate(velocity * Time.deltaTime);
        
        // Rotate child towards velocity
        Vector3 rotationAmount = Vector3.Cross(transform.forward, desired);
        rb.angularVelocity = rotationAmount * torque;
        rb.velocity = desired * force;


    }
}
