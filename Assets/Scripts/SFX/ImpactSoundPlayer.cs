using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSoundPlayer : MonoBehaviour
{
    public AudioClip[] impactSounds; // Array of audio clips
    private AudioSource audioSource; // The AudioSource component

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomImpactSound()
    {
        if (impactSounds.Length > 0)
        {
            // Pick a random index from the array of sounds
            int randomIndex = Random.Range(0, impactSounds.Length);

            // Play the randomly selected sound
            audioSource.PlayOneShot(impactSounds[randomIndex]);
        }
    }
}