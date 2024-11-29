using System;
using UnityEngine;
using UnityEngine.Serialization;

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
[ExecuteInEditMode]
public class RadarPing : MonoBehaviour
{
    [SerializeField] private QBezier bezier = new(0f, 0.8f, 1f, 0f);
    
    public float maxDistance = 200.0f;
    public Transform origin;
    public GameObject target;
    
    [SerializeField] private GameObject XZ;
    [SerializeField] private Transform ping;
    [SerializeField] private Transform positiveY;
    [SerializeField] private Transform negativeY;
    // Update is called once per frame
    void Update()
    {
        if (!target) return;
        
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
            // Set ping to not active
            XZ.SetActive(false);
        }
    }
}
