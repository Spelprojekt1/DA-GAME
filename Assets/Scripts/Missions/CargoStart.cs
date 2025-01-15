using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoStart : MonoBehaviour
{
    public int cargo = 10;
    private PlayerCargoManager playerCargoManager;
    [SerializeField] private GameObject cargoEnd;
    [SerializeField] private LockTarget targetLocker;

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
        targetLocker.SetLock(cargoEnd);
        if (cargo >= amount)
        {
            cargo -= amount;
            return amount;
        }
        else
        {
            int temp = cargo;
            cargo = 0;
            playerCargoManager.SetSource(null);
            return temp;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (cargo > 0 && other.CompareTag("Player"))
        {
            playerCargoManager = other.GetComponent<PlayerCargoManager>();
            playerCargoManager.SetSource(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCargoManager>().SetSource(null);
        }
    }
}
