using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserBehavior : MonoBehaviour
{
    
    private float lifeTime = 4f;
    private string targetTag = "Player";
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            other.gameObject.GetComponent<PlayerBehaviour>().Hurt(new Damage(10f,1f,0.5f));
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
