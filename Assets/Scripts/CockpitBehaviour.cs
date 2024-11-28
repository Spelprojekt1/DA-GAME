using UnityEngine;
using UnityEngine.UI;

public class CockpitBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerMovement playerMovement;

    [SerializeField] private Slider thrust;
    [SerializeField] private Slider reverseThrust;

    [SerializeField] private Image pointer;
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
        transform.rotation = player.transform.rotation;
        pointer.rectTransform.localPosition = new Vector3(
            playerMovement.Rotation.z * -200,
            playerMovement.Rotation.x * -200,
            0);
    }
}
