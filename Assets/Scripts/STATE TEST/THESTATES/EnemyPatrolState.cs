using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    public Color rayColor = Color.black;
    public Color rayColorPatrol = Color.magenta;
   GetVariable()
    public override void EnterState(EnemyStateManager enemy)
    {
        rayColor = rayColorPatrol;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        Vector3 pos = patrolTarget.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, patrolRotationalDamp * Time.deltaTime);
    }

    public override void OnCollisionEnter(EnemyStateManager enemy)
    {
        
    }
}
