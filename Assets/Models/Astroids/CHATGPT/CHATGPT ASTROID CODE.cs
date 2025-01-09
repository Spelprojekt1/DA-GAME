using UnityEngine;

public class ApplyAngularVelocity : MonoBehaviour
{
    public Vector3 angularVelocity; // Angular velocity to apply (in radians per second)
    public ParticleSystem particleSystem;  // Reference to the Particle System
    public float particleSpeed = 5f; // Control speed of particle movement (optional)
    public AudioClip[] collisionSounds; // Array of collision sound effects
    public AudioSource audioSource; // AudioSource to play the sounds

    private Rigidbody rb;
    private Transform particleTransform;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Apply the initial angular velocity to the Rigidbody
            rb.angularVelocity = angularVelocity;

            // Optional: Check if the Rigidbody is set to Kinematic
            if (rb.isKinematic)
            {
                Debug.LogWarning("Rigidbody is set to Kinematic. Please uncheck 'Is Kinematic' for physics-based movement.");
            }
        }
        else
        {
            Debug.LogWarning("No Rigidbody found on the object.");
        }

        // Unparent the particle system (optional)
        if (particleSystem != null)
        {
            // Get the Particle System's Transform
            particleTransform = particleSystem.transform;

            // Unparent the particle system from the asteroid (i.e., set its parent to null)
            particleTransform.SetParent(null);

            // Optionally, reset the position of the particle system
            particleTransform.position = transform.position;
        }
        else
        {
            Debug.LogWarning("No Particle System assigned.");
        }

        // Ensure AudioSource is assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogWarning("No AudioSource found or assigned.");
            }
        }
    }

    void Update()
    {
        // User input for controlling the asteroid's angular velocity
        float x = Input.GetAxis("Horizontal");  // Use left/right arrow or A/D for X axis
        float y = Input.GetAxis("Vertical");    // Use up/down arrow or W/S for Y axis
        float z = Input.GetAxis("Fire1");       // Use any other input for Z axis (e.g., Left Control or mouse button)

        // Set angular velocity based on input
        angularVelocity = new Vector3(x, y, z);

        // Optionally, set angular velocity only if it's not zero
        if (angularVelocity.magnitude > 0)
        {
            if (rb != null)
            {
                rb.angularVelocity = angularVelocity; // Apply the updated angular velocity
            }
        }

        // User input for controlling the particle system's movement (independent of asteroid)
        if (particleTransform != null)
        {
            // Example: Use arrow keys or WASD to move the particle system
            float particleX = Input.GetAxis("Horizontal") * particleSpeed * Time.deltaTime;
            float particleY = Input.GetAxis("Vertical") * particleSpeed * Time.deltaTime;

            // Apply movement to the particle system (this will move it independently of the asteroid)
            particleTransform.position += new Vector3(particleX, particleY, 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check for collision with any object
        if (collision.gameObject != null)
        {
            // Play the particle system
            if (particleSystem != null)
            {
                // Set the particle system's position to the asteroid's position
                particleSystem.transform.position = transform.position;

                // Play the particle system
                particleSystem.Play();
            }

            // Play a random collision sound
            if (collisionSounds != null && collisionSounds.Length > 0 && audioSource != null)
            {
                // Choose a random sound from the collection
                int randomIndex = Random.Range(0, collisionSounds.Length);
                audioSource.PlayOneShot(collisionSounds[randomIndex]);
            }

            // Destroy the asteroid game object
            Destroy(gameObject);
        }
    }
}