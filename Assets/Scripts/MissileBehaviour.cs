using UnityEngine;
using UnityEngine.Serialization;

public class MissileBehaviour : MonoBehaviour
{
    // [SerializeField] private Vector3 velocity;
    [SerializeField] private float force = 10f;
    [SerializeField] private float torque = 2f;
    
    // Target to home in on
    public Transform target;
    public float lifeTime = 5f;
    private Rigidbody rb;
    public string targetTag = "Enemy";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * force;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Destroy(gameObject);
        }
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
        Vector3 rotationAmount = Vector3.Cross(transform.forward, desired);
        rb.angularVelocity = rotationAmount * torque;
        rb.velocity = transform.forward * force;
    }
}
