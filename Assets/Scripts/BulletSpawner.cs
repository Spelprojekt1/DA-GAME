using UnityEditor;
using UnityEngine;

public class BulletSpawner : ProjectileSpawner
{
    public override string Name => "canons";
    [Tooltip("Bullet will fly for this many seconds before despawning")]
    [SerializeField] private float lifeTime = 2f;
    private float cooldown = 0f;
    [SerializeField] private float startCooldown = 0.2f;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private GameObject bulletPrefab;
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
        if (cooldown <= 0)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
            bulletBehaviour.lifeTime = lifeTime;
            bulletBehaviour.targetTag = targetTag;
            bulletBehaviour.startVelocity = playerRb.velocity + transform.forward * bulletSpeed; 
            cooldown = startCooldown;
        }
    }
}
