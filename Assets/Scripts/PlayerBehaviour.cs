using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private Transform target;
    private ProjectileSpawner[] primaryWeapons;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float shield;
    [SerializeField] private float maxShield;
    [Tooltip("shield per second that's regenerated")]
    [SerializeField] private float shieldRegen;
    public float Health => health;
    public float MaxHealth => maxHealth;
    public float Shield => shield;
    public float MaxShield => maxShield;
    
    // Start is called before the first frame update
    void Start()
    {
        ProjectileSpawner[] spawners = GetComponentsInChildren<ProjectileSpawner>();
        primaryWeapons = spawners.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (shield > maxShield) shield = maxShield;
        if (shield < maxShield) shield += shieldRegen * Time.deltaTime;
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
