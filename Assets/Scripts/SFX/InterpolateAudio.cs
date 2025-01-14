using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Audio sources paired with a value representing their place along the interpolation
[Serializable]
struct AudioPoint
{
    [Tooltip("The audio source to interpolate")]
    public AudioSource AudioSource;
    [Tooltip("The maximum volume of the audio source")]
    [Range(0,1)]public float MaxVolume;
    [Tooltip("The value at which the audio will start to fade in")]
    [Range(0,1)]public float FadeInAt;
    [Tooltip("The length of the fade before max volume is reached")]
    [Range(0,1)]public float FadeInLength;
    [Tooltip("The value at which the audio will start to fade out")]
    [Range(0,1)]public float FadeOutAt;
    [Tooltip("The length of the fade before the audio stops playing")]
    [Range(0,1)]public float FadeOutLength;
    public AudioPoint(AudioSource audioSource, float maxVolume, float fadeInAt, float fadeInLength, float fadeOutAt, float fadeOutLength)
    {
        AudioSource = audioSource;
        MaxVolume = maxVolume;
        FadeInAt = fadeInAt;
        FadeInLength = fadeInLength;
        FadeOutAt = fadeOutAt;
        FadeOutLength = fadeOutLength;
    }
}
public class InterpolateAudio : MonoBehaviour
{
    [SerializeField] private PlayerMovement drivingScript;
    [SerializeField]private float currentValue;
    [Tooltip("If true, negative values will be treated as positive")]
    [SerializeField] private bool absoluteProperty;
    [Tooltip("The minimum value of the property (should be a positive number if absolute property is true)")]
    [SerializeField] private float minValue;
    [Tooltip("The maximum value of the property")]
    [SerializeField] private float maxValue;
    [SerializeField] private List<AudioPoint> audioPoints;
    
    void OnValidate()
    {
        for (int i = 0; i < audioPoints.Count; i++)
        {
            if (audioPoints[i].FadeOutAt < audioPoints[i].FadeInAt)
            {
                audioPoints[i] = new AudioPoint(
                    audioPoints[i].AudioSource,
                    audioPoints[i].MaxVolume,
                    audioPoints[i].FadeInAt,
                    audioPoints[i].FadeInLength,
                    audioPoints[i].FadeInAt,
                    audioPoints[i].FadeOutLength
                );
            }
            if (audioPoints[i].FadeOutAt + audioPoints[i].FadeOutLength > 1)
            {
                audioPoints[i] = new AudioPoint(
                    audioPoints[i].AudioSource,
                    audioPoints[i].MaxVolume,
                    audioPoints[i].FadeInAt,
                    audioPoints[i].FadeInLength,
                    audioPoints[i].FadeOutAt,
                    1 - audioPoints[i].FadeOutAt
                );
            }
            if (audioPoints[i].FadeInAt + audioPoints[i].FadeInLength > audioPoints[i].FadeOutAt)
            {
                audioPoints[i] = new AudioPoint(
                    audioPoints[i].AudioSource,
                    audioPoints[i].MaxVolume,
                    audioPoints[i].FadeInAt,
                    audioPoints[i].FadeOutAt - audioPoints[i].FadeInAt,
                    audioPoints[i].FadeOutAt,
                    audioPoints[i].FadeOutLength
                );
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentValue = Mathf.InverseLerp(minValue, maxValue, absoluteProperty ?
                Mathf.Abs(drivingScript.InterpolateAudioValue) :
                drivingScript.InterpolateAudioValue);
        
        foreach (AudioPoint audio in audioPoints)
        {
            audio.AudioSource.volume = Mathf.Lerp(0, audio.MaxVolume, Mathf.Min(
                audio.FadeInAt + audio.FadeInLength == 0 ? 1 :
                Mathf.InverseLerp(audio.FadeInAt, audio.FadeInAt + audio.FadeInLength, currentValue),   
                1 - Mathf.InverseLerp(audio.FadeOutAt, audio.FadeOutAt + audio.FadeOutLength, currentValue)
            ));
            
        }
    }
}
