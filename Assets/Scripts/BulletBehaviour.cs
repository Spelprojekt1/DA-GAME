using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [Header("Values filled by spawner")]
    public float lifeTime;
    private Rigidbody rb;
    public string targetTag;
    public Vector3 startVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = startVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            other.gameObject.GetComponent<OldEnemyMovement>().Hurt(new Damage(5f,0.5f,1f));
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
    }
}
