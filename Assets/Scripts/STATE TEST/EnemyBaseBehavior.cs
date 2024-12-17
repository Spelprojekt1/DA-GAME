using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseBehavior : MonoBehaviour
{
    public Transform currentEnemyTarget;
    [SerializeField] public Rigidbody rigidBody;
    [SerializeField] public bool projectileReload;
    [SerializeField] public float projectileTimer = 2f;
    [SerializeField] public Rigidbody enemyLaser;
    [SerializeField] public Transform rightProjectileSpawner;
    [SerializeField] public Transform leftProjectileSpawner;
    public bool avoidTerrain = false;
    public bool patrol = false;
    public bool chasePlayer = false;
    public bool repositionAway = false;
    //public bool  = false;
    public bool doShootBulletMethod = false;
    [SerializeField] public float projectileSpeed = 10f;
    //The player Gameobject 
    // [SerializeField] public GameObject player;

    //What target the enemy will move towards
    [SerializeField] public Transform patrolTarget;

    //Player Target when player gets to close. 
    [SerializeField] public Transform playerTarget;

    //TEST TURN AGAINST PLAYER//
    //[SerializeField] private float enemyTurningSpeed = 1f;

    //private Coroutine lookCoroutine;

    // [SerializeField]public float turningSpeed = 2f;
    //ENEMY RAY VARS AND ENEMY MOVEMENT
    //The default color
    public Color rayColor = Color.black;
    //Colors for diffrent enemy states
    public Color rayColorPatrol = Color.magenta;
    public Color rayColorChase = Color.red;
    public Color rayColorReposition = Color.yellow;
    public Color rayColorAvoidTerrain = Color.green;

    [SerializeField] public float playerRotationalDamp = .7f;
    [SerializeField] public float rayCastOffset = 2.5f;
    [SerializeField] public float detectionDistance = 20f;
    [SerializeField] public float movementSpeed = 7f;
    [SerializeField] public float patrolRotationalDamp = .5f;
    //ENEMY PLAYER DETECTION DISTANCE VARS 
    //Timer för hur lång tid player behöver vara inom "chasePlayerRange" för att enemy ska börja fokusera player
    [SerializeField] public float chasePlayerTimerLength = 2.0f;

    public bool chasePlayerTimer;

    public bool patrolMode;

    //Hur nära player kan vara till enemyes börjar fokusera på player istället
    [SerializeField] public float maxChasePlayerRange = 150.0f;
    [SerializeField] public float chasePlayerRange = 100.0f;
    [SerializeField] public float fightPlayerRange = 35.0f;

    [SerializeField] public float repositionAwayFromPlayerRange = 20.0f;
    [SerializeField] public float smoothRotation = 1.0f;

    //private Coroutine LookCoroutine;
    //Hur långt det är emellan player och enemy
    public float distanceBetween = 0f;

    // Update is called once per frame
    void ShootBullet()
    {
        //Shoots 2 Bullet forwards
        var projectileRight = Instantiate(enemyLaser, rightProjectileSpawner.transform.position, transform.rotation);
        projectileRight.velocity = transform.forward * projectileSpeed;
        var projectileLeft = Instantiate(enemyLaser, leftProjectileSpawner.transform.position, transform.rotation);
        projectileLeft.velocity = transform.forward * projectileSpeed;
        //Reseting values ( reload cooldown ) 
            
        projectileReload = false;
        projectileTimer = 2f;
        doShootBulletMethod = false;
    }
    void Update()
    {
        if (avoidTerrain)
        {
            rayColor = rayColorAvoidTerrain;

        }
        else if (patrol)
        {
            rayColor = rayColorPatrol;
        }

        if (doShootBulletMethod = true)
        {
            ShootBullet();
        }



    
        
        RaycastHit hit;
        Vector3 raycastOffset = Vector3.zero;

        //Med 4 Vector3 skapar formen för 4 raycast (leftRay,rightRay,upRay,downRay) kan öka rayCastOffset
        Vector3 leftRay = transform.position - transform.right * rayCastOffset;
        Vector3 rightRay = transform.position + transform.right * rayCastOffset;
        Vector3 upRay = transform.position + transform.up * rayCastOffset;
        Vector3 downRay = transform.position - transform.up * rayCastOffset;

        //Ritar ut dom här 4 rays,
        Debug.DrawRay(leftRay, transform.forward * detectionDistance, rayColor);
        Debug.DrawRay(rightRay, transform.forward * detectionDistance, rayColor);
        Debug.DrawRay(upRay, transform.forward * detectionDistance, rayColor);
        Debug.DrawRay(downRay, transform.forward * detectionDistance, rayColor);
//drive towards the target and turn left/right or up/down when ray detects a obstacale  
        if (Physics.Raycast(leftRay, transform.forward, out hit, detectionDistance))
        {
            avoidTerrain = true;
            raycastOffset += Vector3.right;
            //rayColor = rayColorAvoidTerrain;

        }
        else if (Physics.Raycast(rightRay, transform.forward, out hit, detectionDistance))
        {
            avoidTerrain = true;
            raycastOffset -= Vector3.right;
           // rayColor = rayColorAvoidTerrain;
           

        }

        if (Physics.Raycast(upRay, transform.forward, out hit, detectionDistance))
        {
            avoidTerrain = true;
            raycastOffset -= Vector3.up;
            
            // rayColor = rayColorAvoidTerrain;
            //avoidTerrain = true
        }
        else if (Physics.Raycast(downRay, transform.forward, out hit, detectionDistance))
        {
            avoidTerrain = true;
            raycastOffset += Vector3.up;
           // rayColor = rayColorAvoidTerrain;
            //avoidTerrain = true

        }

        if (raycastOffset != Vector3.zero)
        {
            // Debug.Log("nu?");
            transform.Rotate(raycastOffset * 3f * Time.deltaTime);
        }
        //DecideTarget();
        //Pathfinding();
        Move();
        //Kollar konstant avståndet emellan enemy till player
        distanceBetween = (playerTarget.transform.position - transform.position).magnitude;
        //distanceBetween = (playerTarget.transform.position - transform.position).magnitude;

        void Move()
        {
            //Enemy rör sig frammåt hela tiden.
            rigidBody.AddForce(transform.forward * (Time.deltaTime * movementSpeed));
           
        } 
         

        //void Pathfinding()
        //{




            
           
        }
    }
    


