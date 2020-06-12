using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{
	public Dialogue[] dialogue;

	public int currentDialogue;

	public MinigameManager minigameManager;

	public Transform CameraPosition;

	//Starts interacting with the player
	public override void Interact()
	{
		TriggerDialogue();
	}

	//Starts the dialog
	public void TriggerDialogue()
	{
		//CameraController cameraController = minigameManager.mainCamera.GetComponent<CameraController>();
		//cameraController.offset = cameraPosition;
		//cameraController.target = minigame.transform;

		FindObjectOfType<DialogueManager>().StartDialogue(this);
	}
}
