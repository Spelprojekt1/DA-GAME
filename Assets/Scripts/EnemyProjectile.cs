using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public Rigidbody enemyLaser;
    public float shootSpeed = 100;
    private bool playerInRange = false;
    private float lastAttackTime = 0f;
    private float fireRate = 0.5f; 
    private Transform player = null;
	 
  
    void Update()
    {
        if (playerInRange)
        {
            //Rotate the enemy towards the player
            transform.rotation = Quaternion.LookRotation(player.position - transform.position, transform.up);
			 
            if (Time.time - lastAttackTime >= 1f/fireRate)
            {
                shootBullet();
                lastAttackTime = Time.time;
            }
        }
    }
	 
    void shootBullet()
    {
        
        var projectile = Instantiate(enemyLaser, transform.position, transform.rotation);
        //Shoot the Bullet in the forward direction of the player
        projectile.velocity = transform.forward * shootSpeed;
    }
    
}
