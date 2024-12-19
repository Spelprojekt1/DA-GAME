using UnityEditor;
using UnityEngine;
public enum RadarPingType
{
    ENEMY
}
public class Radar : MonoBehaviour
{
    [SerializeField] private float maxDistance = 200f;
    [SerializeField] private Transform origin;
    [SerializeField] private GameObject RadarPing;
    [SerializeField] private LockTarget targetLocker;
    private GameObject target;
    public void Ping()
    {
        // Remove all children
        for (int i = 0; i < transform.childCount; i++)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        // Get all objects with the tag "RadarEnemy"
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            GameObject ping = Instantiate(RadarPing);
            RadarPing script = ping.GetComponent<RadarPing>();
            script.target = o;
            script.origin = origin;
            script.maxDistance = maxDistance;
            script.type = RadarPingType.ENEMY;
            script.CheckLock(target);
            
            ping.transform.SetParent(transform, false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Ping();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLock(GameObject target)
    {
        this.target = target;
        Ping();
        // For each child find the RadarPing script and run CheckLock
        // for (int i = 0; i < transform.childCount; i++)
        // {
            // RadarPing script = transform.GetChild(i).GetComponent<RadarPing>();
            // script.CheckLock(target);
        // }
    }
}
