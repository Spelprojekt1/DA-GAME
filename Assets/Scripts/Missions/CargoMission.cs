using System.Collections.Generic;
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
    public override List<WayPoint> WayPoints
    {
        get
        {
            List<WayPoint> wayPoints = new List<WayPoint>
            {
                new WayPoint(start.gameObject, "Origin"),
                new WayPoint(end.gameObject, "Destination")
            };
            
            foreach (var cargo in looseCargo)
            {
                wayPoints.Add(new WayPoint(cargo.Item2, $"Loose Cargo {cargo.Item1}"));
            }
            return wayPoints;
        }
    }

    public CargoMission(string name, string description, int reward, CargoStart start, CargoEnd end, int cargo) : base(name, description, reward)
    {
        this.start = start;
        this.end = end;
        totalCargo = cargo;
        startCargo = cargo;
        endCargo = 0;
    }
}
