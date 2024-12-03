
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEditor.ShaderGraph;

public class EnemyMovement : MonoBehaviour
{
    private Transform currentEnemyTarget;

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
    private Color rayColor = Color.black;
    
    private Color rayColorPatrol = Color.magenta;
    private Color rayColorChase = Color.red;
    private Color rayColorAvoidTerrain = Color.green;

    [SerializeField] public float rotationalDamp = .5f;
    [SerializeField] public float rayCastOffset = 2.5f;
    [SerializeField] public float detectionDistance = 20f;
    [SerializeField] public float movementSpeed = 7f;

    //ENEMY PLAYER DETECTION DISTANCE VARS 
    //Hur nära player kan vara till enemyes börjar fokusera på player istället
    [SerializeField] public float maxChasePlayerRange = 150.0f;
    [SerializeField] public float chasePlayerRange = 100.0f;
    [SerializeField] public float fightPlayerRange = 70.0f;

    [SerializeField] public float repositionAwayFromPlayerRange = 20.0f;

    //Hur långt det är emellan player och enemy
    public float distanceBetween = 0f;

    // Update is called once per frame
    void Update()
    {
        
        // Om spelaren är inom en vissa radie av enemy så ska enemy börja följa spelaren (enemyTarget = playerTarget)
        
        Pathfinding();
        Move();


        void Turn()
        {
          //  if (currentEnemyTarget = patrolTarget)
            {
              //  Vector3 pos = patrolTarget.position - transform.position;
              //  Quaternion rotation = Quaternion.LookRotation(pos);
               // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
            }
            if (currentEnemyTarget = playerTarget)
                // THE ROTATE TOWARDS PLAYER FUNCTION
                transform.LookAt(playerTarget);
              //  transform.rotation = Quaternion.Slerp(transform.rotation, rotation2, rotationalDamp * Time.deltaTime);
        }

        void TargetPlayer()
        {
            // THE ROTATE TOWARDS PLAYER FUNCTION
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, playerTarget.rotation, 50.0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
        }

        void Move()
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
        }

        void Pathfinding()
        {
            
            //Kollar konstant avståndet emellan enemy till player
            distanceBetween = (playerTarget.transform.position - transform.position).magnitude;
            
            

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
                raycastOffset += Vector3.right;
                rayColor = rayColorAvoidTerrain;

            }
            else if (Physics.Raycast(rightRay, transform.forward, out hit, detectionDistance))
            {
                raycastOffset -= Vector3.right;
                rayColor = rayColorAvoidTerrain;

            }

            if (Physics.Raycast(upRay, transform.forward, out hit, detectionDistance))
            {
                raycastOffset -= Vector3.up;
                rayColor = rayColorAvoidTerrain;

            }
            else if (Physics.Raycast(downRay, transform.forward, out hit, detectionDistance))
            {
                raycastOffset += Vector3.up;
                rayColor = rayColorAvoidTerrain;

            }

            if (raycastOffset != Vector3.zero)
            {
                Debug.Log("nu?");
                transform.Rotate(raycastOffset * 3f * Time.deltaTime);
            }
            else
            {
                Turn();
                //TargetPlayer();
            }
            
            if (distanceBetween < chasePlayerRange)
            {
                currentEnemyTarget = playerTarget;
                rayColor = rayColorChase;
            }
            else 
            {
                currentEnemyTarget = patrolTarget;
                rayColor = rayColorPatrol;
            }
            //Debugging medelanden 
            Debug.Log("Enemies distance between player: "+distanceBetween);
            Debug.Log("Enemies current target: " +currentEnemyTarget);
           // Debug.Log(rayColor);
        }
    }
}
