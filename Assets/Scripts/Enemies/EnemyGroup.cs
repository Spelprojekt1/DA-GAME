using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public int EnemyCount { get; set; }
    public int MaxEnemyCount { get; private set; }
    private int nonEnemyChildren;
    // Start is called before the first frame update
    void Start()
    {
        // Count the number of children with the tag "Enemy"
        nonEnemyChildren = transform.childCount - transform.GetComponentsInChildren<Transform>().Where(child => child.CompareTag("Enemy")).Count();
        MaxEnemyCount = transform.childCount - nonEnemyChildren;
        EnemyCount = MaxEnemyCount;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyCount = transform.childCount - nonEnemyChildren;
    }
}
