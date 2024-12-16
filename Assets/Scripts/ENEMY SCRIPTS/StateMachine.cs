using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    BaseState currentState;
    
    void Start()
    {
        currentState = GetInitialState();
    }

  
    void Update()
    {
        if (currentState != null)
            currentState.UpdateLogic();
    }
    void LateUpdate()
    {
        if (currentState != null)
            currentState.UpdatePhysics();
    }
    //Transition to new states function. 
    public void ChangeState(BaseState newState)
    {
        currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }

    private void OnGUI()
    {
        string content = currentState != null ? currentState.name : "(no active state)";
        GUILayout.Label($"<color='white'><size=40>(content)</size<>/color>");
    }
}
