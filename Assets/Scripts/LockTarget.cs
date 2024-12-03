using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class LockTarget : MonoBehaviour
{
    [SerializeField] private float LockRange = 100;
    [SerializeField] private GameObject lockedEnemy;
    public UnityEvent<GameObject> TargetLocked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * LockRange, Color.green);
    }
    public void TryLock()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, LockRange))
        {
            if (hit.collider.tag == "Enemy")
            {
                lockedEnemy = hit.collider.gameObject;
                TargetLocked.Invoke(lockedEnemy);
            }
        }
    }
}
