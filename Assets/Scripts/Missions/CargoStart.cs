using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoStart : MonoBehaviour
{
    [SerializeField] private int cargo = 10;
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
        if (cargo >= amount)
        {
            cargo -= amount;
            return amount;
        }
        else
        {
            int temp = cargo;
            cargo = 0;
            return temp;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCargoManager>().SetSource(this);
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
