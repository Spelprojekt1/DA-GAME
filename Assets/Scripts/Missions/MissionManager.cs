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
    [SerializeField] private ApplicationHandler appHandler;
    [SerializeField] private int startDebt;
    [SerializeField] private int debt;
    [SerializeField] public List<AMission> activeMissions { get; private set; }
    [SerializeField] private List<CargoMissionParams> cargoMissionsList;
    [SerializeField] private List<CombatMissionParams> combatMissionsList;

    public int Debt => debt;
    void OnValidate()
    {
        RefreshActiveMissions();
    }

    // Start is called before the first frame update
    void Start()
    {
        RefreshActiveMissions();
        debt = startDebt;
    }

    void RefreshActiveMissions()
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
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < activeMissions.Count; i++)
        {
            if (activeMissions[i].Completion >= 1)
            {
                debt -= activeMissions[i].Reward;
                activeMissions.Remove(activeMissions[i]);
            }
        }

        if (debt <= 0)
        {
            // Load the scene named "WinScene" and unlock cursor
            Cursor.lockState = CursorLockMode.None;
            appHandler.ChangeScene("WinScene");
        }
    }
}
