using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    //private EnemyBaseBehavior enemyBaseBehavior;
    public override void EnterState(EnemyStateManager enemy)
    {

        enemy.projectileTimer = 5f;
        Debug.Log("ChaseState Active");
        
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        // THE ROTATE TOWARDS PLAYER FUNCTION
        //enemy.transform.LookAt(enemy.playerTarget); OLD ROTATION 
        
        // NEW ROTATE TOWARDS PLAYER ATTEMPT
        Vector3 pos1 = enemy.playerTarget.position - enemy.transform.position;
        Quaternion rotation1 = Quaternion.LookRotation(pos1);
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, rotation1, enemy.playerRotationalDamp * Time.deltaTime);
       
        //enemy.rayColor = enemy.rayColorChase;
        // ENEMY SHOOTING CODE
        if (enemy.distanceBetween < enemy.startShootRange)
        {
            enemy.projectileTimer -= Time.deltaTime;
            if (enemy.projectileTimer <= 0.0f)
            {
                enemy.projectileReload = true;

            }
        }

        if (enemy.projectileReload)
        {
            // the enemy fires the bullets now in the EnemyBaseBehavior Script.
            enemy.enemyBaseBehaviorScript.ShootBullet();
            enemy.projectileTimer = 5f;
            enemy.projectileReload = false;

        }
        //---TRANSITIONS TO OTHER STATES---
        
        //REPOSITIONING STATE ( WILL COME BACK TO CHASE STATE WHEN DONE IN REPO STATE)
        if(enemy.distanceBetween < enemy.repositionAwayFromPlayerRange)
        {enemy.SwitchState(enemy.RepositionState);}
        
        //PATROL STATE
        else if (enemy.distanceBetween > enemy.chasePlayerRange)
        {
            enemy.chasePlayerTimer = true;
            enemy.chasePlayerTimerLength = Time.time;
            enemy.SwitchState(enemy.PatrolState);
        }
    
    }

    public override void OnCollisionEnter(EnemyStateManager enemy)
    {
        
    }
}
