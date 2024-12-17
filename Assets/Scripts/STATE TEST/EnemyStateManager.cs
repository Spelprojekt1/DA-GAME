using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    
    //Alla State scripts som finns
    EnemyBaseState currentState;
    EnemyChaseState ChaseState = new EnemyChaseState();
    EnemyPatrolState PatrolState = new EnemyPatrolState();
    EnemyRepositionState RepositionState = new EnemyRepositionState();
    
    void Start()
    {
        //The state that the enemy always starts in the patrolState
        currentState = PatrolState;
        currentState.EnterState(this); 
    }

   
    void Update()
    {
        currentState.UpdateState(this);
    }
}
