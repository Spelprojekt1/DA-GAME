using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] private float health = 100f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float shield = 100f;
    [SerializeField] private float maxShield = 100f;
    [Tooltip("shield per second that's regenerated")]
    [SerializeField] private float shieldRegen = 1f;
    
    //What target the enemy will move towards
    [SerializeField]public Transform target;
    [SerializeField]public float movementSpeed = 7f;
   // [SerializeField]public float turningSpeed = 2f;
    [SerializeField]public float rotationalDamp = .5f;
    [SerializeField]public float rayCastOffset = 2.5f;
    [SerializeField] public float detectionDistance = 20f;


   

    // Update is called once per frame
    void Update()
    {
        if (shield < maxShield) shield += shieldRegen * Time.deltaTime;
        if (shield > maxHealth) shield = maxHealth;
        
        Pathfinding();
     
        Move();

    }

    public void Hurt(Damage dam)
    {
        float damage = dam.Dam;
        if (damage * dam.Smod < shield)
        {
            shield -= damage * dam.Smod;
            return;
        }
        if (shield > 0)
        {
            damage -= shield / dam.Smod;
            shield = 0;
        }
        health -= damage * dam.Amod;
        if (health <= 0) Destroy(gameObject);
    }

    void Turn()
    {
       //Debug.Log( "Target");
        Vector3 pos = target.position - transform.position;
       Quaternion rotation = Quaternion.LookRotation(pos);
       transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
    }

    void Move()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    void Pathfinding()
    {
        RaycastHit hit;
        Vector3 raycastOffset = Vector3.zero;
        
        //Med 4 Vector3 skapar formen för 4 raycast (leftRay,rightRay,upRay,downRay) kan öka rayCastOffset
        Vector3 leftRay = transform.position - transform.right * rayCastOffset;
        Vector3 rightRay = transform.position + transform.right * rayCastOffset;
        Vector3 upRay = transform.position + transform.up * rayCastOffset;
        Vector3 downRay = transform.position - transform.up * rayCastOffset;
       
        //Ritar ut dom här 4 rays,
        Debug.DrawRay(leftRay, transform.forward * detectionDistance, Color.magenta);
        Debug.DrawRay(rightRay, transform.forward * detectionDistance, Color.magenta);
        Debug.DrawRay(upRay, transform.forward * detectionDistance, Color.red);
        Debug.DrawRay(downRay, transform.forward * detectionDistance, Color.red);
       
        //drive towards the target and turn left/right or up/down when ray detects a obstacale  
        if (Physics.Raycast(leftRay, transform.forward, out hit, detectionDistance))
        {
            raycastOffset += Vector3.right;
        }
        else if (Physics.Raycast(rightRay, transform.forward, out hit, detectionDistance))
        {
            raycastOffset -= Vector3.right;
            //Debug.Log( "vänster");
        }
        if (Physics.Raycast(upRay, transform.forward, out hit, detectionDistance))
        {
            raycastOffset -= Vector3.up ;
            //Debug.Log( "ner");
        }
        else if (Physics.Raycast(downRay, transform.forward, out hit, detectionDistance))
        {
            raycastOffset += Vector3.up;
            //Debug.Log( "upp");
        }
        
        if(raycastOffset != Vector3.zero)
        {transform.Rotate(raycastOffset * 3f * Time.deltaTime);}
        else
        {
            Turn();
        }
        
    }
}
