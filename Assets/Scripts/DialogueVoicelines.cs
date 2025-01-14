using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string text;  // Dialogtext
        public AudioClip voiceLine;  // Voiceline kopplad till texten
    }

    public List<DialogueLine> dialogueLines = new List<DialogueLine>(11);  // Lista över dialoglinjer med 11 element
    public Text dialogueText;  // UI-element för texten
    public AudioSource audioSource;  // AudioSource för att spela upp voicelines

    private int currentLineIndex = 0;

    void Start()
    {
        if (dialogueLines.Count > 0)
        {
            PlayDialogueLine(currentLineIndex);
        }
    }

    public void NextDialogueLine()
    {
        if (currentLineIndex < dialogueLines.Count - 1)
        {
            currentLineIndex++;
            PlayDialogueLine(currentLineIndex);
        }
        else
        {
            EndDialogue();
        }
    }

    private void PlayDialogueLine(int index)
    {
        DialogueLine line = dialogueLines[index];
        dialogueText.text = line.text;  // Visa dialogtext

        if (audioSource != null && line.voiceLine != null)
        {
            audioSource.clip = line.voiceLine;  // Ladda voiceline
            audioSource.Play();  // Spela upp voiceline
        }
    }

    private void EndDialogue()
    {
        dialogueText.text = "";  // Rensa texten
        if (audioSource != null)
        {
            audioSource.Stop();
        }
        Debug.Log("Dialogen är slut.");
    }
}