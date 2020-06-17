﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEgoDialog : DialogueTrigger
{
	protected override void InitValues()
	{
		base.InitValues();

		//Starts interacting with the player
		Invoke(nameof(StartDialogAfterTime), .5f);
	}

	void StartDialogAfterTime()
	{
		TriggerDialogue();
		FindObjectOfType<DialogueManager>().minigameManager = minigameManager;
	}
}
