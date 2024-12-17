using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerBehaviour : MonoBehaviour
{
    private Transform target;
    private Dictionary<string,Stack<ProjectileSpawner>> spawners;
    private int weaponMode;
    private string[] weaponModes;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float shield;
    [SerializeField] private float maxShield;
    [Tooltip("shield per second that's regenerated")]
    [SerializeField] private float shieldRegen;
    private const float SWITCH_COOLDOWN = 0.3f;
    private float switchCooldown = SWITCH_COOLDOWN;
    public float Health => health;
    public float MaxHealth => maxHealth;
    public float Shield => shield;
    public float MaxShield => maxShield;
    public string WeaponMode => weaponModes[weaponMode];
    
    // Start is called before the first frame update
    void Start()
    {
        spawners = new();
        foreach (ProjectileSpawner spawner in GetComponentsInChildren<ProjectileSpawner>())
        {
            if (!spawners.ContainsKey(spawner.Name))
            {
                spawners.Add(spawner.Name,new Stack<ProjectileSpawner>());
            }
            spawners[spawner.Name].Push(spawner);
        }
        weaponModes = spawners.Keys.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (shield > maxShield) shield = maxShield;
        if (shield < maxShield) shield += shieldRegen * Time.deltaTime;
        if (switchCooldown > 0) switchCooldown -= Time.deltaTime;
    }
    public void OnPrimary()
    {
        foreach (var spawner in spawners[weaponModes[weaponMode]])
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
        if (switchCooldown <= 0)
        {
            weaponMode = (weaponMode + 1) % weaponModes.Length;
            switchCooldown = SWITCH_COOLDOWN;
        }
    }

}
