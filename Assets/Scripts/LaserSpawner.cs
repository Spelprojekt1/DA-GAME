using UnityEditor;
using UnityEngine;

public class LaserSpawner : ProjectileSpawner
{
    [SerializeField] private float startDuration = 0.2f;
    private float duration = 0f;
    [SerializeField] private float startCooldown = 0.2f;
    private float cooldown = 0f;
    private bool active = false;

    [SerializeField] private GameObject laser;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else
        {
            if (active)
            {
                active = false;
                cooldown = startCooldown;
                laser.SetActive(false);
            }

            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
        }
    }
    
    public override void Fire()
    {
        if (!active && cooldown <= 0)
        {
            laser.SetActive(true);
            active = true;
            duration = startDuration;
        }
    }
}
