using UnityEngine;

public class TargetHologram : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject intercepter;
    [SerializeField] private GameObject corvette;
    [SerializeField] private GameObject outpost;
    [SerializeField] private GameObject cargo;
    private Transform targetTransform;
    private GameObject hologram;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set hologram to the first child of the hologram object
        hologram = cargo;
    }

    public void OnTargetLocked(GameObject target)
    {
        if (target)
        {
            if (target.CompareTag("Enemy"))
            {
                switch (target.name)
                {
                    default:
                    case "Intercepter":
                    {
                        hologram = intercepter;
                        break;
                    }
                    case "Corvette":
                    {
                        hologram = corvette;
                        break;
                    }
                    
                }
            }
            else
            {
                if (target.GetComponent<CargoEnd>() != null)
                {
                    hologram = outpost;
                }

                if (target.GetComponent<CargoStart>() != null)
                {
                    hologram = cargo;
                }
            }
            targetTransform = target.transform;
            active = true;
            hologram.SetActive(true);
        }
        else
        {
            active = false;
            hologram.SetActive(false);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // Set hologram's rotation to the enemy's rotation relative to the player's viewpoint
        if (active && targetTransform != null)
        {
            hologram.transform.localRotation = Quaternion.Inverse(playerTransform.rotation) * targetTransform.rotation;
        }
    }
}
