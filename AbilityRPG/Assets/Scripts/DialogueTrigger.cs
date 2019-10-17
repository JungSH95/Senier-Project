using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public Transform dialogueTransform;
    public GameObject dialogueBox;
    

    public void TriggerDialogue(bool isAutoDialogue)
    {
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogue, dialogueTransform, isAutoDialogue);
        dialogueBox.GetComponent<DialogueManager>().StartDialogue(dialogue, dialogueTransform, isAutoDialogue);
    }
}
