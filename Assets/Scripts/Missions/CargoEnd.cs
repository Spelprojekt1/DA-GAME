using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoEnd : MonoBehaviour
{
    [SerializeField] private int cargo = 0;
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
            other.GetComponent<PlayerCargoManager>().SetDestination(this);
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
