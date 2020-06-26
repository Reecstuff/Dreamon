using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DialogueTrigger : Interactable
{
	public Dialogue[] dialogue;

	public int currentDialogue;

	public MinigameManager minigameManager;


	public Transform camPosition;

	protected DialogueManager dialogueManager;


	protected override void InitValues()
	{
		base.InitValues();
		dialogueManager = FindObjectOfType<DialogueManager>();
	}

	//Starts interacting with the player
	public override void Interact()
	{
		TriggerDialogue();
	}

	//Starts the dialog
	public void TriggerDialogue()
	{
		if (isClick == false && !dialogueManager.currentDialogObject || !dialogueManager.selectMinigame)
		{
			dialogueManager.StartDialogue(this);
			isClick = true;
		}
	}

	public virtual void TheEnd()
	{
		// Do Something
	}

	protected virtual void SetInactive()
	{
		gameObject.SetActive(false);
	}
}
