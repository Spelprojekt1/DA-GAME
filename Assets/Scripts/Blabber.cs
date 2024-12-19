using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

// Basic script to display a linear dialogue, from a text file, using a TextMeshPro text component.
// NOTE: The Canvas of the text component need to hold only the dialogue related UI!
//       The reason being that Blabber hides the Canvas when the dialogue is over.

public class Blabber : MonoBehaviour
{
    [Tooltip("The TextMeshPro text displaying dialogue lines.")]
    public TextMeshProUGUI lineUI;

    // These events are invoked when the dialogue starts / ends.
    // Can be useful to (in)activate the player's movement etc.
    public UnityEvent onDialogueStarted, onDialogueEnded;

    private Canvas dialogueCanvas;
    private Queue<string> upcomingLines;
    [SerializeField] private TextAsset tutorialText;

    // Before the first frame update we hide the dialogue canvas (UI)
    void Start()
    {
        dialogueCanvas = lineUI.canvas;
        dialogueCanvas.enabled = true;
        StartTalking(tutorialText);
    }

    // Used to start the dialogue of a "manuscript file" (ordinary text file).
    // Each line in the text file represents a dialogue line.
    public void StartTalking(TextAsset manuscript)
    {
        // Fetch the lines from the provided manuscript TextAsset
        var lines = manuscript.text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length == 0)
        {
            Debug.Log("No dialogue lines in file: " + manuscript.name);
            return; // Exits the method
        }
        // Put the lines into a Queue, and update the UI text
        upcomingLines = new Queue<string>(lines);
        lineUI.text = upcomingLines.Dequeue();
        // Show the dialogue UI canvas and invoke event
        dialogueCanvas.enabled = true;
        onDialogueStarted.Invoke();
    }

    // Shows the next dialogue line, if there's lines left. Otherwise closes the dialogue.
    // Intended as a target for a Unity event (like pressing a button).
    public void ProgressDialogue(InputAction.CallbackContext context)
    {
        // Only run progress dialogue upon button press, and if showing the dialogue canvas
        if (context.started && dialogueCanvas.enabled)
        {
            // Show next line in the UI text, if possible
            if (upcomingLines.Count > 0)
            {
                lineUI.text = upcomingLines.Dequeue();
            }
            // Otherwise close the dialogue (no more lines left)
            else
            {
                onDialogueEnded.Invoke();
            }
        }
    }
}
