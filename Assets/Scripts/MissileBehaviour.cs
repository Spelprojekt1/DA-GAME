using UnityEngine;
using UnityEngine.Serialization;

public class MissileBehaviour : MonoBehaviour
{
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
            Vector3 desired = (target.position - transform.position).normalized;
            Vector3 rotationAmount = Vector3.Cross(transform.forward, desired);
            rb.angularVelocity = rotationAmount * torque;
        }        
        rb.velocity = transform.forward * force;
    }
}
