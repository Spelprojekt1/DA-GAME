using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

// [ExecuteInEditMode]
public class Radar : MonoBehaviour
{
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
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("RadarEnemy"))
        {
            GameObject ping = (GameObject)PrefabUtility.InstantiatePrefab(RadarPing);
            ping.GetComponent<RadarPing>().target = o;
            ping.GetComponent<RadarPing>().origin = origin;
            ping.transform.SetParent(transform);
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
