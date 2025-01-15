using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public int EnemyCount { get; set; }
    public int MaxEnemyCount { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        MaxEnemyCount = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyCount = transform.childCount;
    }
}
