using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{   
    // THIS IS THE DEFAULT STATE FOR ALL ENEMIES
    //private EnemyBaseBehavior enemyBaseBehavior;

    
    
    // public Transform patrolTarget; 
   // public float patrolRotationalDamp = .5f;
    //rayColor = Color.black;
    //rayColorPatrol = Color.magenta;
    
    //EnemyBaseBehavior enemyScript;
    
    public override void EnterState(EnemyStateManager enemy)
    {
        //enemy.patrol = true;
        Debug.Log("PatrolState Active");
    }

    public override void UpdateState(EnemyStateManager enemy)
    { 
        
        //enemy.rayColor = enemy.rayColorPatrol;
        Vector3 pos = enemy.patrolTarget.position - enemy.transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, rotation, enemy.patrolRotationalDamp * Time.deltaTime);
        
        // TRANSITIONS TO OTHER STATES
        // GOING INTO ATTACK STATE
        if (enemy.distanceBetween < enemy.chasePlayerRange && !enemy.chasePlayerTimer)
        {
            enemy.chasePlayerTimerLength -= Time.deltaTime;
            if (enemy.chasePlayerTimerLength <= 0.0f)
            {

                enemy.chasePlayerTimer = true;
                enemy.chasePlayerTimerLength = Time.time;

            }
        }
        else if (enemy.distanceBetween < enemy.chasePlayerRange && enemy.distanceBetween > enemy.fightPlayerRange && enemy.chasePlayerTimer)
        {
            //GO TO ENEMY CHASESTATE NOW!
            enemy.SwitchState(enemy.ChaseState);


        }
    }
    
    
    
    

    public override void OnCollisionEnter(EnemyStateManager enemy)
    {
        
    }
}
