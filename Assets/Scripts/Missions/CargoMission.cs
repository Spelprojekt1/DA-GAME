using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CargoMission : AMission
{
    private int totalCargo;
    private CargoStart start;
    private int startCargo;
    private CargoEnd end;
    private int endCargo;
    private Stack<(int,GameObject)> looseCargo = new();
    public override float Completion => (float)endCargo / totalCargo;
    public override GameObject Target
    {
        get
        {
            if (looseCargo.Count > 0)
            {
                return looseCargo.Last().Item2;
            }
            if (startCargo > 0)
            {
                return start.gameObject;
            }
            return end.gameObject;
        }
    }

    public CargoMission(CargoStart start, CargoEnd end, int cargo)
    {
        this.start = start;
        this.end = end;
        totalCargo = cargo;
        startCargo = cargo;
        endCargo = 0;
    }
}
