using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

// [ExecuteInEditMode]
public class Radar : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private float MAX_DISTANCE = 200.0f;
    private const float bezierP1 = 0.0f;
    [SerializeField] private float bezierP2 = 0.8f;
    private const float bezierP3 = 1f;
    private float bezierT = 0f;
    [SerializeField] private GameObject RadarPing;
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private List<GameObject> radarEnemies = new List<GameObject>();
    public void Ping()
    {
        // Remove all children
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    DestroyImmediate(transform.GetChild(i).gameObject);
        //}
        //enemies.Clear();
        //radarEnemies.Clear();

        // Get all objects with the tag "RadarEnemy"
        //enemies = GameObject.FindGameObjectsWithTag("RadarEnemy").ToList();
        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject ping = (GameObject)PrefabUtility.InstantiatePrefab(RadarPing);
            ping.transform.SetParent(transform);
            radarEnemies.Add(ping);
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
        //Ping();
        if (enemies.Count == 0) return;
        if (radarEnemies.Count == 0) return;

        Vector3 originPosition = origin.position;
        for (int i = 0; i < enemies.Count; i++)
        {
            Vector3 target = enemies[i].transform.position;
            Debug.Log(target);
            Debug.Log(Mathf.Abs((target - originPosition).magnitude) < MAX_DISTANCE);
            if (Mathf.Abs((target - originPosition).magnitude) < MAX_DISTANCE)
            {
                // Set ping to active
                radarEnemies[i].SetActive(true);

                // Calculate bezierT
                bezierT = (target - originPosition).magnitude / MAX_DISTANCE;

                // Calculate one dimensional quadratic bezier curve
                float a = (1 - bezierT) * bezierP1 + bezierT * bezierP2;
                float b = (1 - bezierT) * bezierP2 + bezierT * bezierP3;
                float c = (1 - bezierT) * a + bezierT * b;

                // Use bezier curve to scale the ping's distance on the radar
                Vector3 rPingVector = (target - originPosition).normalized * c;

                // Rotate rPingVector opposite to origin's rotation
                rPingVector = Quaternion.Inverse(origin.transform.rotation) * rPingVector;

                // Move ping to rPingVector and account for radar scaling
                radarEnemies[i].GetComponent<RadarPing>().UpdatePing(Vector3.Scale(rPingVector, transform.lossyScale));
            }
            else
            {
                // Set ping to not active
                radarEnemies[i].SetActive(false);
            }
        }
    }
}
