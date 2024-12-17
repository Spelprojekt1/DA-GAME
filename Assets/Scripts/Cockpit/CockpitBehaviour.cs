using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CockpitBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerMovement playerMovement;
    private PlayerBehaviour playerBehaviour;
    [SerializeField] private Image thrust;
    [SerializeField] private Image reverseThrust;
    [SerializeField] private Image health;
    [SerializeField] private Image shield;
    [SerializeField] private float lowerMargin = 0.07f;
    [SerializeField] private float upperMargin = 0.07f;
    [SerializeField] private Slider targetHealth;
    [SerializeField] private Slider targetShield;
    [SerializeField] private Image pointer;
    [SerializeField] private GameObject targetPointer;
    [SerializeField] private TextMeshProUGUI weaponMode;
    [SerializeField] private TextMeshProUGUI hostilesCount;
    private OldEnemyMovement target;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        thrust.fillAmount = Mathf.Lerp(lowerMargin, 0.5f - upperMargin, playerMovement.Thrust);
        reverseThrust.fillAmount = Mathf.Lerp(lowerMargin, 0.5f - upperMargin, -playerMovement.Thrust);
        health.fillAmount = Mathf.Lerp(lowerMargin, 0.5f - upperMargin, playerBehaviour.Health / playerBehaviour.MaxHealth);
        shield.fillAmount = Mathf.Lerp(lowerMargin, 0.5f - upperMargin, playerBehaviour.Shield / playerBehaviour.MaxShield);
        weaponMode.text = playerBehaviour.MissileMode ? "MISSILES" : "LASERS";

        hostilesCount.text = GameObject.FindGameObjectsWithTag("Enemy").Length.ToString();

        if (target)
        {
            targetHealth.value = target.Health / target.MaxHealth;
            targetShield.value = target.Shield / target.MaxShield;

            targetPointer.transform.localRotation = Quaternion.Inverse(player.transform.rotation) * Quaternion.LookRotation(target.transform.position - player.transform.position);
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
            this.target = target.GetComponent<OldEnemyMovement>();
            targetHealth.gameObject.SetActive(true);
            targetShield.gameObject.SetActive(true);
            targetPointer.SetActive(true);
        }
        else
        {
            this.target = null;
            targetHealth.gameObject.SetActive(false);
            targetShield.gameObject.SetActive(false);
            targetPointer.SetActive(false);
        }
    }
}
