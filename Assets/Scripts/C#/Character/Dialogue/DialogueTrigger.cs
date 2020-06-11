using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{
	public Dialogue[] dialogue;

	public int currentDialogue;

	public MinigameManager minigameManager;

	//Starts interacting with the player
	public override void Interact()
	{
		GetComponent<DialogueTrigger>().TriggerDialogue();
	}

	//Starts the dialog
	public void TriggerDialogue()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(this);
	}
}
