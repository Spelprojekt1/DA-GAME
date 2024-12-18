using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMission : AMission
{
    private EnemyGroup enemyGroup;
    public override float Completion => 1 - (float)enemyGroup.EnemyCount / enemyGroup.MaxEnemyCount;
    public override Vector3 Location => enemyGroup.transform.position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
