using UnityEngine;

public class CockpitBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;   
    [SerializeField] private GameObject targetPointer;
    private GameObject target;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            targetPointer.transform.localRotation = Quaternion.Inverse(player.transform.rotation) * Quaternion.LookRotation(target.transform.position - player.transform.position);
        }
        
        transform.rotation = player.transform.rotation;
    }

    public void OnTargetLocked(GameObject target)
    {
        this.target = target;
        targetPointer.SetActive(target != null);
    }
}
