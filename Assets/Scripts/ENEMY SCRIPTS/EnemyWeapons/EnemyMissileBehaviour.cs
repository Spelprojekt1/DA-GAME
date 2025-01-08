using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMissileBehaviour : MonoBehaviour
{
    [Tooltip("Acceleration of the missile")]
    [SerializeField] private float force = 10f;
    [Tooltip("How fast the missile can turn")]
    [SerializeField] private float torque = 2f;
    [Tooltip("How far in front of the target the missile should aim. Setting the value to 0 will result in the missile orbiting the target.")]
    [SerializeField] private float targetPositionOffset = 5f;

    
    [Header("Values filled by spawner")]
    // Target to home in on
    public Transform target;
    private float lifeTime = 5f;
    private Rigidbody rb;
    //private Transform = enemy.playerTarget.position;
       
    private string targetTag = "Player";
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
            other.gameObject.GetComponent<PlayerBehaviour>().Hurt(new Damage(20f,0.5f,1f));
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
        
        
            Vector3 desired = (target.position - (transform.forward * targetPositionOffset) - transform.position).normalized;
            Vector3 rotationAmount = Vector3.Cross(transform.forward, desired);
            rb.angularVelocity = rotationAmount * torque;
                
        rb.velocity += transform.forward * force * Time.deltaTime;
    }
}

