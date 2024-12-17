using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerMovement playerMovement;
    [SerializeField] private Image pointer;
    [SerializeField] private GameObject targetReticle;
    private OldEnemyMovement target;
    [SerializeField] private TextMeshProUGUI hostilesCount;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hostilesCount.text = GameObject.FindGameObjectsWithTag("Enemy").Length.ToString();
        
        if (target)
        {
            targetReticle.transform.position =
                Camera.main.WorldToScreenPoint(target.transform.position, Camera.MonoOrStereoscopicEye.Mono);
        }
        
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
            targetReticle.SetActive(true);
        }
        else
        {
            this.target = null;
            targetReticle.SetActive(false);
        }
    }
}
