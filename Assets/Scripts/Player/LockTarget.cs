using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class LockTarget : MonoBehaviour
{
    [SerializeField] private float LockRange = 100;
    [SerializeField] private GameObject lockedEnemy;
    public UnityEvent<GameObject> TargetLocked;
    private bool locked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (locked)
        {
            if (!lockedEnemy)
            {
                // Feedback
                Debug.Log("Target Lost");
                locked = false;
                TargetLocked.Invoke(null);
            }
        }
    }
    public void TryLock()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, LockRange))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                lockedEnemy = hit.collider.gameObject;
                TargetLocked.Invoke(lockedEnemy);
                locked = true;
            }
        }
    }

    public void SetLock(GameObject target)
    {
        lockedEnemy = target;
        TargetLocked.Invoke(lockedEnemy);
        locked = true;
        Debug.Log("Locked " + target.name + " With position " + target.transform.position);
    }
}
