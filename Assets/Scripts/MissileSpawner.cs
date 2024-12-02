using UnityEditor;
using UnityEngine;

public class MissileSpawner : ProjectileSpawner
{
    [Tooltip("Missile will fly for this many seconds before exploding")]
    [SerializeField] private float missileLifeTime = 3f;
    private float cooldown = 0f;
    [Tooltip("The missile knows where it is at all times. It knows this because it knows where it isn't. By subtracting where it is from where it isn't or where it isn't from where it is (whichever is greater), it obtains a difference, or deviation. The guidance subsystem uses deviations to generate corrective commands to drive the missile from a position where it is to a position where it isn't and arriving at a position where it wasn't, it now is.")]
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }
    
    public override void Fire()
    {
        Debug.Log("Firing missile");
        if (cooldown <= 0)
        {
            GameObject missile = (GameObject)PrefabUtility.InstantiatePrefab(missilePrefab);
            missile.transform.position = transform.position;
            missile.transform.rotation = transform.rotation;
            MissileBehaviour missileBehaviour = missile.GetComponent<MissileBehaviour>();
            missileBehaviour.target = target;
            missileBehaviour.lifeTime = missileLifeTime;
        }
    }
}