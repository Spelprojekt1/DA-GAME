//Written by AI
using UnityEngine;

public class ApplyAngularVelocityToParent : MonoBehaviour
{
    public float angularVelocityMagnitude = 1f; // Magnitude of angular velocity (in radians per second)
    private Rigidbody rb; // Rigidbody of the parent object
    private ParticleSystem particleSystem; // Reference to the ParticleSystem

    void Start()
    {
        // Get the Rigidbody component of the parent object
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Generate a completely random angular velocity vector
            Vector3 randomAngularVelocity = Random.onUnitSphere * angularVelocityMagnitude;
            rb.angularVelocity = randomAngularVelocity;
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
}
