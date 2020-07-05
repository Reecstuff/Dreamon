using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEgoDialog : DialogueTrigger
{
	protected override void InitValues()
	{
		base.InitValues();

		//Starts interacting with the player
		Invoke(nameof(StartDialogAfterTime), .1f);
	}

	void StartDialogAfterTime()
	{
		TriggerDialogue();
		FindObjectOfType<DialogueManager>().minigameManager = minigameManager;
	}

	public override void TheEnd(bool isLose)
	{
		base.TheEnd(isLose);
		SetInactive();
	}
}
