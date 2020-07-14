using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEgoDialog : DialogueTrigger
{
	[SerializeField]
	TimedTalk[] chessTalk;


	protected override void InitValues()
	{
		base.InitValues();

		//Starts interacting with the player
		Invoke(nameof(StartDialogAfterTime), .1f);
	}

	void StartDialogAfterTime()
	{
		TriggerDialogue();
		dialogueManager.minigameManager = minigameManager;
		dialogueManager.player.GetComponent<CallBetweenText>().CallBetween(chessTalk);
	}

	public override void TheEnd(bool isLose)
	{
		base.TheEnd(isLose);
		dialogueManager.player.GetComponent<CallBetweenText>().EndCall();
		SetInactive();
	}
}
