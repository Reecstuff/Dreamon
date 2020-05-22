using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{
	public Dialogue dialogue;

	public override void Interact()
	{
		GetComponent<DialogueTrigger>().TriggerDialogue();
	}

	public void TriggerDialogue()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(this);
	}

	//public void SetOpinion()
	//{
	//	FindObjectOfType<DialogueManager>().SelectOpinion(dialogue);
	//}
}
