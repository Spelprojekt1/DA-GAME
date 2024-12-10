using UnityEngine;
using UnityEngine.Serialization;

public class MissileBehaviour : MonoBehaviour
{
    [Tooltip("Acceleration of the missile")]
    [SerializeField] private float force = 15f;
    [Tooltip("How fast the missile can turn")]
    [SerializeField] private float torque = 3f;
    [Tooltip("How far in front of the target the missile should aim. Setting the value to 0 will result in the missile orbiting the target.")]
    [SerializeField] private float targetPositionOffset = 5f;
    
    [Header("Values filled by spawner")]
    // Target to home in on
    public Transform target;
    public float lifeTime;
    private Rigidbody rb;
    public string targetTag;
    public Vector3 startVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = startVelocity + transform.forward * force;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            other.gameObject.GetComponent<EnemyMovement>().Hurt(new Damage(75f,0.5f,1f));
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Decrease lifetime
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
            return;
        }
        lifeTime -= Time.deltaTime;

        // Home in on target if it exists
        if (target)
        {
            Vector3 desired = (target.position - (transform.forward * targetPositionOffset) - transform.position).normalized;
            Vector3 rotationAmount = Vector3.Cross(transform.forward, desired);
            rb.angularVelocity = rotationAmount * torque;
        }        
        rb.velocity += transform.forward * force * Time.deltaTime;
    }
}
