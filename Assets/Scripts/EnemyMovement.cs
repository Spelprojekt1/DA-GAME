
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEditor.ShaderGraph;
using UnityEngine.Serialization;

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
    //Timer för hur lång tid player behöver vara inom "chasePlayerRange" för att enemy ska börja fokusera player
    [SerializeField] public float chasePlayerTimerLength = 2.0f;
    private bool chasePlayerTimer;
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
        Debug.Log(chasePlayerTimer);
        Debug.Log(chasePlayerTimerLength);
        // Om spelaren är inom en vissa radie av enemy så ska enemy börja följa spelaren (enemyTarget = playerTarget)
        DecideTarget();
        Pathfinding();
        Move();


        void TargetPatrol()
        {
            // if (currentEnemyTarget = patrolTarget)
            // {
            rayColor = rayColorPatrol;
            Vector3 pos = patrolTarget.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(pos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
        }

        // }// if (currentEnemyTarget = playerTarget)}
        void ChasePlayer()
        {
            rayColor = rayColorChase;
            
        }
        void TargetPlayer()
        {
            // THE ROTATE TOWARDS PLAYER FUNCTION
            rayColor = rayColorChase;
            transform.LookAt(playerTarget);
            //  transform.rotation = Quaternion.Slerp(transform.rotation, rotation2, rotationalDamp * Time.deltaTime)
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
                //avoidTerrain = true
            }
            else if (Physics.Raycast(downRay, transform.forward, out hit, detectionDistance))
            {
                raycastOffset += Vector3.up;
                rayColor = rayColorAvoidTerrain;
                //avoidTerrain = true

            }

            if (raycastOffset != Vector3.zero)
            {
                Debug.Log("nu?");
                transform.Rotate(raycastOffset * 3f * Time.deltaTime);
            }
            else
            {
                TargetPatrol();
                //TargetPlayer();
            }
        }

        //Bestämmmer vilken "state" enemy byter till
        void DecideTarget()
        {
            //CHECK IF TARGET IS PLAYER
            if (distanceBetween < chasePlayerRange && !chasePlayerTimer)
            {
                chasePlayerTimerLength -= Time.deltaTime;
                if (chasePlayerTimerLength <= 0.0f)
                {
                    
                    chasePlayerTimer = true;
                    
                    
                }


            }
            else if (distanceBetween < chasePlayerRange && chasePlayerTimer)
            {
                TargetPlayer();
               

            }
            //CHECK IF TARGET IS PATROLTAGET
            else if (distanceBetween > chasePlayerRange && chasePlayerTimer)
            {
                TargetPatrol();
                //chasePlayerTimerLength += 2f;

            }
            else if (distanceBetween > chasePlayerRange)
            {
                TargetPatrol();
                //chasePlayerTimerLength += 2f;

            }

            //Debugging medelanden FÖR DECIDE TARGET
           // Debug.Log("Enemies distance between player: " + distanceBetween);
           // Debug.Log("Enemies current target: " + currentEnemyTarget);
            // Debug.Log(rayColor);}

        }
    }
}

