using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct QBezier
{
    public float P1;
    public float P2;
    public float P3;
    public float T;
    public QBezier(float p1, float p2, float p3, float t)
    {
        P1 = p1;
        P2 = p2;
        P3 = p3;
        T = t;
    }
}
[Serializable]
struct PingMaterial
{
    public Material Primary;
    public Material Secondary;
    public PingMaterial(Material Primary, Material Secondary)
    {
        this.Primary = Primary;
        this.Secondary = Secondary;
    }
}

[Serializable]
struct PingMaterialTyped
{
    public RadarPingType Type;
    
    public Material Primary;
    public Material Secondary;
    public PingMaterialTyped(RadarPingType type, Material primary, Material secondary)
    {
        Type = type;
        Primary = primary;
        Secondary = secondary;
    }
}

[ExecuteInEditMode]
public class RadarPing : MonoBehaviour
{
    [SerializeField] private QBezier bezier = new(0f, 0.8f, 1f, 0f);
    [SerializeField] private PingMaterial lockedTargetMaterials;

    // UNITY CAN'T SERIALIZABLE DICTIONARIES ARGH!! >:(
    [SerializeField] private List<PingMaterialTyped> materials;
    private Dictionary<RadarPingType, PingMaterial> materialsDictionary = new();
    public float maxDistance = 200.0f;
    public Transform origin;
    public GameObject target;
    public RadarPingType type;
    [SerializeField] private bool locked = false;
    [SerializeField] private GameObject XZ;
    [SerializeField] private Transform ping;
    [SerializeField] private Transform positiveY;
    [SerializeField] private Transform negativeY;

    void OnValidate()
    {
        LoadMaterials();
    }
    public void LoadMaterials()
    {
        materialsDictionary.Clear();
        foreach (var material in materials)
        {
            materialsDictionary.Add(material.Type, new PingMaterial(material.Primary, material.Secondary));
        }

        if (!materialsDictionary.ContainsKey(type)) Debug.LogError($"No material for ping type {type}");
        ping.GetComponent<MeshRenderer>().material = materialsDictionary[type].Primary;
        XZ.GetComponent<MeshRenderer>().material = materialsDictionary[type].Secondary;
        positiveY.GetComponent<MeshRenderer>().material = materialsDictionary[type].Secondary;
        negativeY.GetComponent<MeshRenderer>().material = materialsDictionary[type].Secondary;
    }
    void Update()
    {
        if (!target)
        {
            Destroy(gameObject);
            return;
        }
        
        Vector3 targetPos = target.transform.position;
        Vector3 originPos = origin.position;
        if (Mathf.Abs((targetPos - originPos).magnitude) < maxDistance)
        {
            // Set ping to active
            XZ.SetActive(true);

            // Calculate bezierT
            bezier.T = (targetPos - originPos).magnitude / maxDistance;

            // Calculate one dimensional quadratic bezier curve
            float a = (1 - bezier.T) * bezier.P1 + bezier.T * bezier.P2;
            float b = (1 - bezier.T) * bezier.P2 + bezier.T * bezier.P3;
            float c = (1 - bezier.T) * a + bezier.T * b;

            // Use bezier curve to scale the ping's distance on the radar
            Vector3 rPingVector = (targetPos - originPos).normalized * c;

            // Rotate rPingVector opposite to origin's rotation
            rPingVector = Quaternion.Inverse(origin.transform.rotation) * rPingVector;
            XZ.transform.localPosition = new Vector3(rPingVector.x, 0, rPingVector.z);
            ping.transform.localPosition = new Vector3(0, rPingVector.y, 0);
            positiveY.transform.localScale = new Vector3(1, Mathf.Max(0,rPingVector.y), 1);
            negativeY.transform.localScale = new Vector3(1, Mathf.Max(0,-rPingVector.y), 1);
        }
        else
        {
            if (locked)
            {
                // Set ping to active
                XZ.SetActive(true);

                Vector3 rPingVector = (targetPos - originPos).normalized;

                // Rotate rPingVector opposite to origin's rotation
                rPingVector = Quaternion.Inverse(origin.transform.rotation) * rPingVector;
                XZ.transform.localPosition = new Vector3(rPingVector.x, 0, rPingVector.z);
                ping.transform.localPosition = new Vector3(0, rPingVector.y, 0);
                positiveY.transform.localScale = new Vector3(1, Mathf.Max(0,rPingVector.y), 1);
                negativeY.transform.localScale = new Vector3(1, Mathf.Max(0,-rPingVector.y), 1);
            }
            else
            {
                // Set ping to not active
                XZ.SetActive(false);
            }
        }
    }
    public void CheckLock(GameObject target)
    {
        if (this.target == target)
        {
            // if (!locked)
            // {
                ping.GetComponent<MeshRenderer>().material = lockedTargetMaterials.Primary;
                XZ.GetComponent<MeshRenderer>().material = lockedTargetMaterials.Secondary;
                positiveY.GetComponent<MeshRenderer>().material = lockedTargetMaterials.Secondary;
                negativeY.GetComponent<MeshRenderer>().material = lockedTargetMaterials.Secondary;
            // }
            locked = true;
        }
        else
        {
            // if (locked)
            // {
                if (!materialsDictionary.ContainsKey(type)) Debug.LogError($"No material for ping type {type}");

                ping.GetComponent<MeshRenderer>().material = materialsDictionary[type].Primary;
                XZ.GetComponent<MeshRenderer>().material = materialsDictionary[type].Secondary;
                positiveY.GetComponent<MeshRenderer>().material = materialsDictionary[type].Secondary;
                negativeY.GetComponent<MeshRenderer>().material = materialsDictionary[type].Secondary;
            // }
            locked = false;
        }
    }
}
