using UnityEngine;

public class CockpitBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;   
    [SerializeField] private GameObject targetPointer;
    private OldEnemyMovement target;

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            targetPointer.transform.localRotation = Quaternion.Inverse(player.transform.rotation) * Quaternion.LookRotation(target.transform.position - player.transform.position);
        }
        
        transform.rotation = player.transform.rotation;
    }

    public void OnTargetLocked(GameObject target)
    {
        if (target)
        {
            this.target = target.GetComponent<OldEnemyMovement>();
            targetPointer.SetActive(true);
        }
        else
        {
            this.target = null;
            targetPointer.SetActive(false);
        }
    }
}
