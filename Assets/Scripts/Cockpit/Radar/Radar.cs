using System;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;
public enum RadarPingType
{
    ENEMY,
    CARGO_SOURCE,
    CARGO_DESTINATION,
    CARGO_LOOSE
}

[Serializable]
struct PingType
{
    public string Tag;
    public RadarPingType Type;
    public PingType(string tag, RadarPingType type)
    {
        Tag = tag;
        Type = type;
    }
}
public class Radar : MonoBehaviour
{
    [SerializeField] private float maxDistance = 200f;
    [SerializeField] private Transform origin;
    [SerializeField] private GameObject RadarPing;
    [SerializeField] private LockTarget targetLocker;
    [SerializeField] private List<PingType> trackableTags;
    private GameObject target;
    public void Ping()
    {
        // Remove all children
        for (int i = 0; i < transform.childCount; i++)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        // Get all objects with pingable tags
        foreach (var tag in trackableTags)
        {
            foreach (GameObject o in GameObject.FindGameObjectsWithTag(tag.Tag))
            {
                GameObject ping = Instantiate(RadarPing);
                RadarPing script = ping.GetComponent<RadarPing>();
                script.target = o;
                script.origin = origin;
                script.maxDistance = maxDistance;
                script.type = tag.Type;
                script.CheckLock(target);
                
                ping.transform.SetParent(transform, false);
            }
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
    }
}
