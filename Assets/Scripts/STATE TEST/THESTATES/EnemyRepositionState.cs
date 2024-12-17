using UnityEngine;

public class EnemyRepositionState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        Vector3 pos = enemy.patrolTarget.position - enemy.transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, rotation, enemy.patrolRotationalDamp * Time.deltaTime);
        // GOING BACK TO THE CHASE STATE
        if (enemy.distanceBetween > enemy.fightPlayerRange)
        {
            enemy.SwitchState(enemy.ChaseState);
        }
    }

    public override void OnCollisionEnter(EnemyStateManager enemy)
    {
        
    }
}
