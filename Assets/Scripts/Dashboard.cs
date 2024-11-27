using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dashboard : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerMovement playerMovement;

    [SerializeField] private Slider thrust;
    [SerializeField] private Slider reverseThrust;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        thrust.value = playerMovement.Thrust;
        reverseThrust.value = -playerMovement.Thrust;
    }
}
