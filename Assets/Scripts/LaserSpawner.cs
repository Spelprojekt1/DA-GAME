using System;
using UnityEditor;
using UnityEngine;

public class LaserSpawner : ProjectileSpawner
{
    public override string Name => "lasers";
    [SerializeField] private float startDuration = 0.2f;
    private float duration = 0f;
    [SerializeField] private float startCooldown = 0.2f;
    private float cooldown = 0f;
    private bool active = false;
    [SerializeField] private float range = 200f;
    private float length = 200f;
    [SerializeField] private string targetTag = "Enemy";
    [SerializeField] private float dps = 75f;
    [SerializeField] private AudioSource audioSource;
//-------- LASER RAY CODE --------------------
    [SerializeField] private GameObject laser;
    //private float distanceBetween = 0f;
    //private Vector3 newLaserLength, oldLaserLength;
    
    //Skapar laserRay som används i CastRay metod.
    //Ray laserRay;
    //laserThickness som bestämmer tjockleken på players lasers Rays 
    //[SerializeField] private float laserThickness = 0.7f;

    
   // public float laserRayDistance = laser.transform.localScale 

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
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range))
            {
                length = MathF.Abs(Vector3.Magnitude(hit.collider.gameObject.transform.position - transform.position));
                if (hit.collider.gameObject.CompareTag(targetTag))
                {
                    hit.collider.gameObject.GetComponent<OldEnemyMovement>().Hurt(new Damage(dps * Time.deltaTime,1f,0.5f));
                }
            }
            else
            {
                length = range;
            }
            laser.transform.localScale = new Vector3(1f,1f,length);
        }
        else
        {
            if (active)
            {
                //ResetLaserLength();
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
    
    public override void Fire(Transform _)
    {
        if (!active && cooldown <= 0)
        {
            //Castray();
            laser.SetActive(true);
            active = true;
            duration = startDuration;
            audioSource.Play();
        }
    }

    /*void ResetLaserLength()
    {
        oldLaserLength = new Vector3(1f, 1f, 200f);
        laser.transform.localScale = oldLaserLength;
    }
    void Castray()
    {
        //skapar vart laserRay skickas ifrån och hurlångt den är. Lika lång som längden på laser objectets z scale (200)
        this.laserRay = new Ray(transform.position, transform.forward * laser.transform.localScale.z);
        //ritar ut rayen Scenen för debugging
        CheckForColliders();
    }
    void CheckForColliders()
    {
        //träffar laser något object med någon Physics component händer x 
        if (Physics.SphereCast(this.laserRay,laserThickness, out RaycastHit hit))
        {
            // om laserns ray träffar enemy, skriv ut det
            Debug.Log(hit.collider.gameObject.name + " was hit");
            Debug.Log(distanceBetween);
            
            //stänger av lasern när den träffar något object med Physics
            distanceBetween = (hit.collider.gameObject.transform.position - transform.position).magnitude;
            newLaserLength = new Vector3(1f,1f,distanceBetween);
            
            laser.transform.localScale = newLaserLength;
        }     
    }*/
}
