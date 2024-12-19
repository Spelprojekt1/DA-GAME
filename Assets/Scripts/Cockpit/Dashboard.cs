using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dashboard : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerBehaviour playerBehaviour;
    private PlayerMovement playerMovement;
    private PlayerCargoManager playerCargoManager;
    [SerializeField] private Image thrust;
    [SerializeField] private Image reverseThrust;
    [SerializeField] private Image cargo;
    [SerializeField] private Image health;
    [SerializeField] private Image shield;
    [SerializeField] private float lowerMargin = 0.07f;
    [SerializeField] private float upperMargin = 0.07f;
    [SerializeField] private GameObject targetPanel;
    [SerializeField] private Image targetHealth;
    [SerializeField] private Image targetShield;
    [SerializeField] private TextMeshProUGUI weaponMode;
    private EnemyBaseBehavior target;
    // Start is called before the first frame update
    void Start()
    {
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerCargoManager = player.GetComponent<PlayerCargoManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        thrust.fillAmount = Mathf.Lerp(lowerMargin, 0.5f - upperMargin, playerMovement.Thrust);
        reverseThrust.fillAmount = Mathf.Lerp(lowerMargin, 0.5f - upperMargin, -playerMovement.Thrust);
        cargo.fillAmount = Mathf.Lerp(lowerMargin, 0.5f - upperMargin, (float)playerCargoManager.Cargo / playerCargoManager.CargoCapacity);
        health.fillAmount = Mathf.Lerp(lowerMargin, 0.5f - upperMargin, playerBehaviour.Health / playerBehaviour.MaxHealth);
        shield.fillAmount = Mathf.Lerp(lowerMargin, 0.5f - upperMargin, playerBehaviour.Shield / playerBehaviour.MaxShield);
        weaponMode.text = playerBehaviour.WeaponMode;

        if (target)
        {
            targetHealth.fillAmount = Mathf.Lerp(lowerMargin, 0.5f - upperMargin, target.Health / target.MaxHealth);
            targetShield.fillAmount = Mathf.Lerp(lowerMargin, 0.5f - upperMargin, target.Shield / target.MaxShield);
        }
    }
    public void OnTargetLocked(GameObject target)
    {
        if (target)
        {
            this.target = target.GetComponent<EnemyBaseBehavior>();
            targetPanel.SetActive(true);
        }
        else
        {
            this.target = null;
            targetPanel.SetActive(false);
        }
    }
}
