using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    private List<CargoMission> cargoMissions;
    private List<CombatMission> combatMissions;
    public List<AMission> activeMissions;

    // Start is called before the first frame update
    void Start()
    {
        activeMissions = new List<AMission>();
        for (int i = 0; i < 5; i++)
        {
            CargoStart start = new();
            CargoEnd end = new();
            
            activeMissions.Add(new CargoMission(start, end, 50));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
