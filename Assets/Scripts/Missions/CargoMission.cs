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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
