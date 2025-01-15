using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatMission : AMission
{
    private EnemyGroup enemyGroup;
    public override float Completion => 1 - (float)enemyGroup.EnemyCount / enemyGroup.MaxEnemyCount;
    // return a list of waypoints made of the children of the enemyGroup's transform
    public override List<WayPoint> WayPoints {
        get {
            List<WayPoint> wayPoints = new List<WayPoint>();
            foreach (Transform child in enemyGroup.transform)
            {
                wayPoints.Add(new WayPoint(child.gameObject, child.name));
            }

            return wayPoints;
        }
    }

    public CombatMission(string name, string description, int reward, EnemyGroup enemyGroup) : base(name, description, reward)
    {
        this.enemyGroup = enemyGroup;
    }
}
