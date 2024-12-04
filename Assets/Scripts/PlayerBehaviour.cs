using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private Transform target;
    private ProjectileSpawner[] missiles;
    private ProjectileSpawner[] lasers;
    private bool missileMode = false;
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
    public bool MissileMode => missileMode;
    
    // Start is called before the first frame update
    void Start()
    {
        missiles = GetComponentsInChildren<MissileSpawner>();
        lasers = GetComponentsInChildren<LaserSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shield > maxShield) shield = maxShield;
        if (shield < maxShield) shield += shieldRegen * Time.deltaTime;
    }
    public void OnPrimary()
    {
        foreach (var spawner in missileMode? missiles : lasers)
        {
            spawner.Fire(target);
        }
    }
    public void OnTargetLock(GameObject target)
    {
        if (!target) this.target = null;
        else this.target = target.transform;
    }
    public void OnSwitchWeapon()
    {
        missileMode = !missileMode;
    }

}
