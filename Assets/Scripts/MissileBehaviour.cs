using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float speed = 25f;
    [SerializeField] private float rotationSpeed = 50f;
    // Target to home in on
    public Transform target;
    public float lifeTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        velocity = transform.forward * speed;
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
        
        Vector3 desired = (target.position - transform.position).normalized * speed;
        Vector3 steering = (desired - velocity).normalized * rotationSpeed;
        velocity += steering * Time.deltaTime;
        transform.Translate(velocity * Time.deltaTime);
        
        // Rotate towards velocity
    }
}
