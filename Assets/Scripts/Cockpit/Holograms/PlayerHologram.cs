using UnityEngine;

public class PlayerHologram : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Vector3 rotationExaggeration = new (20f, 20f, 20f);
    private Transform hologramTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Set hologramTransform to the first child of the hologram object
        hologramTransform = transform.GetChild(0);
    }
    
    // Update is called once per frame
    void Update()
    {
        hologramTransform.localRotation = Quaternion.Euler(Vector3.Scale(playerMovement.RotationalInput, rotationExaggeration));
    }
}
