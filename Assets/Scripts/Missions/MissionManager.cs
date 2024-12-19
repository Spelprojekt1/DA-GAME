using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    private List<CargoMission> cargoMissions;
    private List<CombatMission> combatMissions;
    public List<AMission> activeMissions { get; private set; }

    // TEMPORARY
    [SerializeField] private List<CargoStart> cargoStarts;
    [SerializeField] private List<CargoEnd> cargoEnds;
    
    // Start is called before the first frame update
    void Start()
    {
        activeMissions = new List<AMission>();
        for (int i = 0; i < 2; i++)
        {
            CargoStart start = cargoStarts[i];
            CargoEnd end = cargoEnds[i];
            
            activeMissions.Add(new CargoMission(start, end, 50));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
