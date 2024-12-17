using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyStateManager : EnemyBaseBehavior
{
    //Får in alla variabler och functioner ifrån EnemyMovement Script
   // public EnemyMovement enemyScript;
   // public EnemyBaseBehavior enemyScript;
    //Alla State scripts som finns
   public  EnemyBaseState currentState;
   public  EnemyChaseState ChaseState = new EnemyChaseState();
   public  EnemyPatrolState PatrolState = new EnemyPatrolState();
   public  EnemyRepositionState RepositionState = new EnemyRepositionState();
    
    void Start()
    {
        //The state that the enemy always starts in the patrolState
        currentState = PatrolState;
        currentState.EnterState(this); 
    }

   
    void Update()
    {
        currentState.UpdateState(this);
        distanceBetween = (playerTarget.transform.position - transform.position).magnitude;
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
