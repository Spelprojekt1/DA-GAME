using UnityEditor;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] private float maxDistance = 200f;
    [SerializeField] private Transform origin;
    [SerializeField] private GameObject RadarPing;
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
            GameObject ping = (GameObject)PrefabUtility.InstantiatePrefab(RadarPing);
            RadarPing script = ping.GetComponent<RadarPing>();
            script.target = o;
            script.origin = origin;
            script.maxDistance = maxDistance;
            
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
}
