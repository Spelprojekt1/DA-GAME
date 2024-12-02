using System.Drawing;
using UnityEditor;
using UnityEngine;

public class LaserSpawner : ProjectileSpawner
{
    [SerializeField] private float startDuration = 0.2f;
    private float duration = 0f;
    [SerializeField] private float startCooldown = 0.2f;
    private float cooldown = 0f;
    private bool active = false;
 
//-------- LASER RAY CODE --------------------
    [SerializeField] private GameObject laser;
    private float distanceBetween = 0f;
    private Vector3 newLaserLength, oldLaserLength;
    
    //Skapar laserRay som används i CastRay metod.
    Ray laserRay;
    //laserThickness som bestämmer tjockleken på players lasers Rays 
    [SerializeField] private float laserThickness = 0.7f;

    
   // public float laserRayDistance = laser.transform.localScale 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
        //Timer tickar ned när duration större än 0
        if (duration > 0)
        {
            
            duration -= Time.deltaTime;
        }
       //
        else
        {
            if (active)
            {
                ResetLaserLength();
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
        //Aktiverar laserRay
        
        if (!active && cooldown <= 0)
        {
            
            Castray();
            laser.SetActive(true);
            active = true;
            duration = startDuration;
            
        }
    }

    void ResetLaserLength()
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
       
            
    }
}
