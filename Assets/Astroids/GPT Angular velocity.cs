using UnityEngine;

public class ApplyAngularVelocityToParent : MonoBehaviour
{
    public Vector3 angularVelocity; // Angular velocity to apply (in radians per second)
    private Rigidbody rb; // Rigidbody of the parent object
    private ParticleSystem particleSystem; // Reference to the ParticleSystem

    void Start()
    {
        // Get the Rigidbody component of the parent object
        rb = GetComponent<Rigidbody>();
        
        if (rb != null)
        {
            // Apply the angular velocity to the parent object only
            rb.angularVelocity = angularVelocity;
        }
        else
        {
            Debug.LogError("No Rigidbody found on the parent object.");
        }

        // Get the ParticleSystem component (if there is one)
        particleSystem = GetComponentInChildren<ParticleSystem>();
        if (particleSystem != null)
        {
            // Change the simulation space to World to prevent it from rotating with the parent
            var main = particleSystem.main;
            main.simulationSpace = ParticleSystemSimulationSpace.World;
        }
    }

    void Update()
    {
        // Optionally, you could modify angular velocity during gameplay (e.g., based on input or other factors)
        if (rb != null)
        {
            rb.angularVelocity = angularVelocity;
        }
    }
}