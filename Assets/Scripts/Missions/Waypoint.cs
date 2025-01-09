using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct WayPoint
{
    public GameObject target;
    public String name;

    public WayPoint(GameObject target, String name)
    {
        this.target = target;
        this.name = name;
    }
}
