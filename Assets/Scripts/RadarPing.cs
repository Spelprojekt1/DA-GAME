using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarPing : MonoBehaviour
{
    
    [SerializeField] private Transform ping;
    [SerializeField] private Transform positiveY;
    [SerializeField] private Transform negativeY;
    // Update is called once per frame
    public void UpdatePing(Vector3 target)
    {
        transform.localPosition = new Vector3(target.x, 0, target.z);
        ping.transform.localPosition = new Vector3(0, target.y, 0);

        positiveY.transform.localScale = new Vector3(1, Mathf.Max(0,target.z), 1);
        negativeY.transform.localScale = new Vector3(1, Mathf.Max(0,-target.z), 1);
    }
}
