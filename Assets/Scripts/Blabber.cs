using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Blabber : MonoBehaviour
{
    [Tooltip("The TextMeshPro text displaying dialogue lines.")]
    public TextMeshProUGUI lineUI;

    [Tooltip("Audio clips for each dialogue line.")]
    public List<AudioClip> voiceLines;  // Lägger till stöd för voicelines

    public AudioSource audioSource;  // AudioSource för att spela upp ljud

    public UnityEvent onDialogueStarted, onDialogueEnded;

    private Canvas dialogueCanvas;
    private Queue<string> upcomingLines;
    [SerializeField] private TextAsset tutorialText;
    private int currentLineIndex = 0;  // För att spåra aktuell dialoglinje
    private bool isPaused = false;     // Spårar om spelet är pausat
    private float pausedTime = 0f;    // Tidsmärkning när ljudet pausas

    void Start()
    {
        dialogueCanvas = lineUI.canvas;
        dialogueCanvas.enabled = true;
        StartTalking(tutorialText);
    }

    public void StartTalking(TextAsset manuscript)
    {
        var lines = manuscript.text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length == 0)
        {
            Debug.Log("No dialogue lines in file: " + manuscript.name);
            return;
        }
        upcomingLines = new Queue<string>(lines);
        currentLineIndex = 0;
        PlayDialogueLine();
        dialogueCanvas.enabled = true;
        onDialogueStarted.Invoke();
    }

    public void ProgressDialogue(InputAction.CallbackContext context)
    {
        if (context.started && dialogueCanvas.enabled)
        {
            if (upcomingLines.Count > 0)
            {
                PlayDialogueLine();
            }
            else
            {
                dialogueCanvas.enabled = false;
                onDialogueEnded.Invoke();
            }
        }
    }

    private void PlayDialogueLine()
    {
        lineUI.text = upcomingLines.Dequeue();

        if (voiceLines != null && currentLineIndex < voiceLines.Count)
        {
            audioSource.clip = voiceLines[currentLineIndex];
            audioSource.Play();
            audioSource.loop = false;
        }
        currentLineIndex++;
    }

    
}
