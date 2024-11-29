using UnityEditor;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class LaserSpawner : ProjectileSpawner
{
    [SerializeField] private float startDuration = 0.2f;
    private float duration = 0f;
    [SerializeField] private float startCooldown = 0.2f;
    private float cooldown = 0f;
    private bool active = false;
   
 

    [SerializeField] private GameObject laser;
    private float laserRayDistance = 0f;
    //Skapar laserRay som används i CastRay metod.
    Ray laserRay;

    
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
        Castray();
        if (!active && cooldown <= 0)
        {
           
           
     
            laser.SetActive(true);
            active = true;
            duration = startDuration;
        }
    }

    void Castray()
    {
        this.laserRay = new Ray(transform.position, transform.forward * laser.transform.localScale.z);
        CheckForColliders();
    }
    void CheckForColliders()
    {
        //träffar laser något object med någon Physics component händer x 
        if (Physics.Raycast(this.laserRay, out RaycastHit hit))
        {
            //Debug.Log(gameObject)
        }
       // RaycastHit hit;
        //Vector3 laserRayDisctance = Vector3.Zero;
        //Skapar en storleken av ray som är lika lång som laserRayDistance.
      //  Vector3 laserRay = transform.position;
      //  //Ritar ut en Röd laserRay rakt fram.
       // Debug.DrawRay(laserRay, transform.forward *laserRayDistance , Color.red);
        //Om laser ray träffar x  så händer x
        //if (Physics.Raycast(laserRay, transform.forward, out hit, laserRayDistance))
       // {
            
    }
}
