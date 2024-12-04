using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CockpitBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerMovement playerMovement;
    private PlayerBehaviour playerBehaviour;
    [SerializeField] private Slider thrust;
    [SerializeField] private Slider reverseThrust;
    [SerializeField] private Slider health;
    [SerializeField] private Slider shield;
    [SerializeField] private Slider targetHealth;
    [SerializeField] private Slider targetShield;
    [SerializeField] private Image pointer;
    [SerializeField] private TextMeshProUGUI weaponMode;
    private EnemyMovement target;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        thrust.value = playerMovement.Thrust;
        reverseThrust.value = -playerMovement.Thrust;
        health.value = playerBehaviour.Health / playerBehaviour.MaxHealth;
        shield.value = playerBehaviour.Shield / playerBehaviour.MaxShield;
        weaponMode.text = playerBehaviour.MissileMode ? "MISSILES" : "LASERS";

        if (target)
        {
            targetHealth.value = target.Health / target.MaxHealth;
            targetShield.value = target.Shield / target.MaxShield;
        }
        
        transform.rotation = player.transform.rotation;
        pointer.rectTransform.localPosition = new Vector3(
            playerMovement.Rotation.z * -200,
            playerMovement.Rotation.x * -200,
            0);
    }

    public void OnTargetLocked(GameObject target)
    {
        if (target)
        {
            this.target = target.GetComponent<EnemyMovement>();
            targetHealth.gameObject.SetActive(true);
            targetShield.gameObject.SetActive(true);
        }
        else
        {
            this.target = null;
            targetHealth.gameObject.SetActive(false);
            targetShield.gameObject.SetActive(false);
        }
    }
}
