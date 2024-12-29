using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AMission
{
    private string name;
    private string description;
    public abstract List<WayPoint> WayPoints { get; }
    private int reward;
    public abstract float Completion { get; }
    
}
