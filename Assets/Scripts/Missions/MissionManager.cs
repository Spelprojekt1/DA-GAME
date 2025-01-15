using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct CargoMissionParams
{
    public string Name;
    public string Description;
    public int Reward;
    public CargoStart Start;
    public CargoEnd End;
    public int Cargo;
}

[Serializable]
struct CombatMissionParams
{
    public string Name;
    public string Description;
    public int Reward;
    public EnemyGroup EnemyGroup;
}

[ExecuteInEditMode]
public class MissionManager : MonoBehaviour
{
    [SerializeField] private int startDebt;
    [SerializeField] private int debt;
    public List<AMission> activeMissions { get; private set; }
    [SerializeField] private List<CargoMissionParams> cargoMissionsList;
    [SerializeField] private List<CombatMissionParams> combatMissionsList;

    public int Debt => debt;
    void OnValidate()
    {
        Start();
    }

    // Start is called before the first frame update
    void Start()
    {
        activeMissions = new List<AMission>();
        for (int i = 0; i < cargoMissionsList.Count; i++)
        {
            activeMissions.Add(new CargoMission(
                cargoMissionsList[i].Name,
                cargoMissionsList[i].Description,
                cargoMissionsList[i].Reward,
                cargoMissionsList[i].Start,
                cargoMissionsList[i].End,
                cargoMissionsList[i].Cargo
            ));
        }
        for (int i = 0; i < combatMissionsList.Count; i++)
        {
            activeMissions.Add(new CombatMission(
                combatMissionsList[i].Name,
                combatMissionsList[i].Description,
                combatMissionsList[i].Reward,
                combatMissionsList[i].EnemyGroup
            ));
        }

        debt = startDebt;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var mission in activeMissions)
        {
            if (mission.Completion >= 1)
            {
                debt -= mission.Reward;
                activeMissions.Remove(mission);
            }
        }
    }
}
