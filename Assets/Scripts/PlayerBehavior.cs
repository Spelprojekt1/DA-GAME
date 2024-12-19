using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private Transform target;
 
    private int weaponMode;
    private string[] weaponModes;
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;
    [SerializeField] public float shield;
    [SerializeField] public float maxShield;
    [Tooltip("shield per second that's regenerated")]
    [SerializeField] public float shieldRegen;
    private const float SWITCH_COOLDOWN = 0.2f;
    private float switchCooldown = SWITCH_COOLDOWN;
    public float Health => health;
    public float MaxHealth => maxHealth;
    public float Shield => shield;
    public float MaxShield => maxShield;
    public string WeaponMode => weaponModes[weaponMode];
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shield > maxShield) shield = maxShield;
        if (shield < maxShield) shield += shieldRegen * Time.deltaTime;
        if (switchCooldown > 0) switchCooldown -= Time.deltaTime;
    }
    public void Hurt(Damage dam)
    {
        float damage = dam.Dam;
        if (damage * dam.Smod < shield)
        {
            shield -= damage * dam.Smod;
            return;
        }
        if (shield > 0)
        {
            damage -= shield / dam.Smod;
            shield = 0;
        }
        health -= damage * dam.Amod;
        if (health <= 0) Destroy(gameObject);
    }
}
