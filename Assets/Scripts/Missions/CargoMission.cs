using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoMission : AMission
{
    private int totalCargo;
    private CargoStart start;
    private int startCargo;
    private CargoEnd end;
    private int endCargo;
    private Stack<(int,Vector3)> looseCargo;
    public override float Completion => (float)endCargo / totalCargo;
    public override Vector3 Location => start.transform.position;

    public CargoMission(CargoStart start, CargoEnd end, int cargo)
    {
        this.start = start;
        this.end = end;
        totalCargo = cargo;
        startCargo = cargo;
        endCargo = 0;
    }
}
