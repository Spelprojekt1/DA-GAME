using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        
        Debug.Log("ChaseState Active");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        // THE ROTATE TOWARDS PLAYER FUNCTION
        enemy.transform.LookAt(enemy.playerTarget);
        //enemy.rayColor = enemy.rayColorChase;
        enemy.projectileTimer -= Time.deltaTime;
        if (enemy.projectileTimer <= 0.0f)
        {
            enemy.projectileReload = true;
                
        }

        if (enemy.projectileReload)
        {
            // the enemy fires the bullets now in the EnemyBaseBehavior Script.
            enemy.doShootBulletMethod = true;

        }
        //TRANSITIONS TO OTHER STATES
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
