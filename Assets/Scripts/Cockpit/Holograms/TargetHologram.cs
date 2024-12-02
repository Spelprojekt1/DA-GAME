using UnityEngine;

public class TargetHologram : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Transform targetTransform;
    private GameObject hologram;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set hologram to the first child of the hologram object
        hologram = transform.GetChild(0).gameObject;
    }

    public void OnTargetLocked(GameObject target)
    {
        if (target != null)
        {
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
        if (active)
        {
            hologram.transform.localRotation = Quaternion.Inverse(playerTransform.rotation) * targetTransform.rotation;
        }
    }
}
