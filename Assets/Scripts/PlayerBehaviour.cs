using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private Transform target;
    private ProjectileSpawner[] primaryWeapons;
    // Start is called before the first frame update
    void Start()
    {
        ProjectileSpawner[] spawners = GetComponentsInChildren<ProjectileSpawner>();
        primaryWeapons = spawners.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPrimary()
    {
        foreach (var spawner in primaryWeapons)
        {
            spawner.Fire(target);
        }
    }
    public void OnTargetLock(GameObject target)
    {
        this.target = target.transform;
    }
}
