using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEgoDialog : DialogueTrigger
{
	protected override void InitValues()
	{
		base.InitValues();

		//Starts interacting with the player
		TriggerDialogue();
		FindObjectOfType<DialogueManager>().minigameManager = minigameManager;
	}
}
