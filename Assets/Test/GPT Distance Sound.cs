using UnityEngine;
using System.Collections.Generic;

public class DistanceBasedSoundSpeed : MonoBehaviour
{
    public List<string> sourceTags; // List of tags to find source objects
    public List<string> targetTags; // List of tags to look for
    public AudioSource audioSource; // The AudioSource component to play the sound effect
    public AudioClip defaultAudioClip; // Default audio clip to use if no AudioSource exists

    public AnimationCurve loopTimeCurve; // Curve to define loop time based on normalized distance
    public float minDistance = 1f; // Minimum distance to consider
    public float maxDistance = 10f; // Maximum distance to consider
    public float cutOffDistance = 15f; // Distance beyond which the sound does not play
    public float delayBetweenLoops = 0.5f; // Delay between the end and start of audio clip

    private float loopTimer = 0f; // Timer to handle looping

    void Start()
    {
        // Ensure an AudioSource exists
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            if (defaultAudioClip != null)
            {
                audioSource.clip = defaultAudioClip;
                audioSource.loop = false; // Disable automatic looping for manual control
            }
            else
            {
                Debug.LogWarning("No default audio clip provided. Please assign an AudioClip.");
            }
        }
    }

    void Update()
    {
        if (sourceTags == null || targetTags == null || audioSource == null)
        {
            Debug.LogWarning("Please assign sourceTags, targetTags, and the audioSource in the inspector.");
            return;
        }

        // Find all source objects with the specified tags
        List<GameObject> sourceObjects = new List<GameObject>();
        foreach (string tag in sourceTags)
        {
            sourceObjects.AddRange(GameObject.FindGameObjectsWithTag(tag));
        }

        // Find the closest target object to any source object
        GameObject closestObject = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject source in sourceObjects)
        {
            foreach (string tag in targetTags)
            {
                GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
                foreach (GameObject obj in taggedObjects)
                {
                    float distance = Vector3.Distance(source.transform.position, obj.transform.position);

                    if (distance < closestDistance && distance <= cutOffDistance)
                    {
                        closestDistance = distance;
                        closestObject = obj;
                    }
                }
            }
        }

        // If no object is within the cutoff distance, stop playing the sound
        if (closestObject == null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            return;
        }

        // Normalize the distance to a 0-1 range
        float normalizedDistance = Mathf.InverseLerp(minDistance, maxDistance, closestDistance);

        // Evaluate the loop time from the curve based on the normalized distance
        float loopTime = loopTimeCurve.Evaluate(normalizedDistance) + delayBetweenLoops;

        // Handle looping based on calculated loop time
        loopTimer -= Time.deltaTime;
        if (loopTimer <= 0f)
        {
            audioSource.Play();
            loopTimer = loopTime;
        }
    }
}
