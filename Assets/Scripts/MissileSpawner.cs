using UnityEditor;
using UnityEngine;

public class MissileSpawner : ProjectileSpawner
{
    [Tooltip("Missile will fly for this many seconds before exploding")]
    [SerializeField] private float lifeTime = 3f;
    private float cooldown = 0f;
    [SerializeField] private float startCooldown = 5f;
    [SerializeField] private Rigidbody playerRb;
    [Tooltip("The missile knows where it is at all times. It knows this because it knows where it isn't. By subtracting where it is from where it isn't or where it isn't from where it is (whichever is greater), it obtains a difference, or deviation. The guidance subsystem uses deviations to generate corrective commands to drive the missile from a position where it is to a position where it isn't and arriving at a position where it wasn't, it now is.")]
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private string targetTag = "Enemy";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }
    
    public override void Fire(Transform target)
    {
        if (target == null)
        {
            // FEEDBACK
            Debug.Log("No active radar lock");
            return;
        }
        if (cooldown <= 0)
        {
            GameObject missile = Instantiate(missilePrefab);
            missile.transform.position = transform.position;
            missile.transform.rotation = transform.rotation;
            MissileBehaviour missileBehaviour = missile.GetComponent<MissileBehaviour>();
            missileBehaviour.target = target;
            missileBehaviour.lifeTime = lifeTime;
            missileBehaviour.targetTag = targetTag;
            missileBehaviour.startVelocity = playerRb.velocity; 
            cooldown = startCooldown;
        }
    }
}