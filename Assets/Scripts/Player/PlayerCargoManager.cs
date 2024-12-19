using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCargoManager : MonoBehaviour
{
    [SerializeField] private CargoStart availableSource;
    [SerializeField] private CargoEnd availableDestination;
    [SerializeField] private int cargoCapacity;
    [SerializeField] private int cargo;
    public int Cargo => cargo;
    public int CargoCapacity => cargoCapacity;
    public bool transferAvailable { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (availableSource != null || availableDestination != null)
        {
            transferAvailable = true;
        }
        else
        {
            transferAvailable = false;
        }
        // IMPLEMENT CARGO FULL CHECK
    }

    public void SetSource(CargoStart source)
    {
        availableSource = source;
    }
    
    public void SetDestination(CargoEnd destination)
    {
        availableDestination = destination;
    }

    public void TransferCargo()
    {
        if (availableDestination != null)
        {
            cargo -= availableDestination.TransferCargo(cargo);
        }
        else if (availableSource != null)
        {
            cargo += availableSource.TransferCargo(cargoCapacity - cargo);
        }
    }
}
