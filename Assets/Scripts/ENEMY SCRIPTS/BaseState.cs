using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
    //checks the name of the other state machines and makes it a string
    public string name;
    protected StateMachine stateMachine;
    
    public BaseState(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }
    //Här kommer Enter, Update och Exit hända
    // kommer användvda olikka typer av updates för att saker ska hända i rätt ordning
        public virtual void Enter() {}
        public virtual void UpdateLogic() {}
        public virtual void UpdatePhysics() {}
        public virtual void Exit() {}
}
