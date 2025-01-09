using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//kebab!?!
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RandomEnemyTargetPosition : MonoBehaviour
{
    [SerializeField] public float randomMaxDistance =10f; 
    [SerializeField] public float randomMinDistance =-10f;

    private float newXPos = 0;
    private float newYPos = 0;
    private float newZPos = 0;
    private float startxPos = 0; 
    private float startyPos = 0;
    private float startzPos = 0;
    
    //The maximun and minimum random thresholds for the timers lenght 
    [SerializeField] public float timerMax =100.0f;
    [SerializeField] public float timerMin =80.0f;

    private float randomTimer = 0;

    private void Start()
    { 
        //Gets the gameObjects starting x,y,z position values and sets xPos, yPos, zPos to that value
        startxPos = transform.localPosition.x; 
        startyPos = transform.localPosition.y; 
        startzPos = transform.localPosition.z;
        
        newXPos = transform.localPosition.x; 
        newYPos = transform.localPosition.y; 
        newZPos = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        //Countdown before new x,y,z positions 
        randomTimer -= Time.deltaTime;
        //Debug.Log(randomTimer);
        if(randomTimer<= 0.0f)
        { NewPositonValues();}
       
        // The new position of the Enemy Target that gets new values from NewPositionValues()
        transform.position = new Vector3(newXPos, newYPos, newZPos);
        
    }

    void NewPositonValues()
    {
        
        //Adds a new random value between the thresholds to every Cordinate from the startPos
        newXPos = startxPos + Random.Range(randomMinDistance, randomMaxDistance);
        newYPos = startyPos + Random.Range(randomMinDistance, randomMaxDistance);
        newZPos = startzPos + Random.Range(randomMinDistance, randomMaxDistance);
        //Resets the timer with a new random value between the thresholds
        randomTimer = Random.Range(timerMin, timerMax);
    }
    
}
