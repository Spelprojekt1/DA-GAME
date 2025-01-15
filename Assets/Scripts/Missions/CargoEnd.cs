using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoEnd : MonoBehaviour
{
    public int cargo = 0;
    public int desiredCargo = 10;
    private PlayerCargoManager playerCargoManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int TransferCargo(int amount)
    {
        cargo += amount;
        return amount;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCargoManager = other.GetComponent<PlayerCargoManager>();
            if (playerCargoManager.Cargo > 0)
            {
                playerCargoManager.SetDestination(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCargoManager>().SetDestination(null);
        }
    }
}
