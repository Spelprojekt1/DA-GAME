using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dashboard : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerBehaviour playerBehaviour;
    private PlayerMovement playerMovement;
    [SerializeField] private Image thrust;
    [SerializeField] private Image reverseThrust;
    [SerializeField] private Image health;
    [SerializeField] private Image shield;
    [SerializeField] private float lowerMargin = 0.07f;
    [SerializeField] private float upperMargin = 0.07f;
    [SerializeField] private Slider targetHealth;
    [SerializeField] private Slider targetShield;
    [SerializeField] private TextMeshProUGUI weaponMode;
    private OldEnemyMovement target;
    // Start is called before the first frame update
    void Start()
    {
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        thrust.fillAmount = Mathf.Lerp(lowerMargin, 0.5f - upperMargin, playerMovement.Thrust);
        reverseThrust.fillAmount = Mathf.Lerp(lowerMargin, 0.5f - upperMargin, -playerMovement.Thrust);
        health.fillAmount = Mathf.Lerp(lowerMargin, 0.5f - upperMargin, playerBehaviour.Health / playerBehaviour.MaxHealth);
        shield.fillAmount = Mathf.Lerp(lowerMargin, 0.5f - upperMargin, playerBehaviour.Shield / playerBehaviour.MaxShield);
        weaponMode.text = playerBehaviour.MissileMode ? "MISSILES" : "LASERS";

        if (target)
        {
            targetHealth.value = target.Health / target.MaxHealth;
            targetShield.value = target.Shield / target.MaxShield;
        }
    }
    public void OnTargetLocked(GameObject target)
    {
        if (target)
        {
            this.target = target.GetComponent<OldEnemyMovement>();
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
