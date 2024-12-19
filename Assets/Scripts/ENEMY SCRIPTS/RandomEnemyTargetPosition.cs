using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemyTargetPosition : MonoBehaviour
{
    [SerializeField] public float randomMax =10f; 
    [SerializeField] public float randomMin =-10f;

    private float xPos = 0;
    private float yPos = 0;
    private float zPos = 0;
    
    //The maximun and minimum random thresholds for the timers lenght 
    [SerializeField] public float timerMax =100.0f;
    [SerializeField] public float timerMin =80.0f;

    private float randomTimer = 0;

    // Update is called once per frame
    void Update()
    {
        //Countdown before new x,y,z positions 
        randomTimer -= Time.deltaTime;
        //Debug.Log(randomTimer);
        if(randomTimer<= 0.0f)
        { NewPositonValues();}
       
        // The new position of the Enemey Target that gets new values in NewPositionValues()
        transform.position = new Vector3(xPos, yPos, zPos);
        
    }

    void NewPositonValues()
    {
        //Gives a new random value between the thresholds to every Cordinate
        xPos = Random.Range(randomMin, randomMax);
        yPos = Random.Range(randomMin, randomMax);
        zPos = Random.Range(randomMin, randomMax);
        //Resets the timer with a new random value between the thresholds
        randomTimer = Random.Range(timerMin, timerMax);
    }
    
}
