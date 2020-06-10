﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellTrigger : Interactable
{
	public bool isWin;

	public GameObject assignedTarget;
	public int winDialogue;
	public int loseDialogue;

	public override void Interact()
	{
		GetComponent<ShellTrigger>().SelectShell();
	}

	public void SelectShell()
	{
		//Play animation

		//Check for winthing
		if (isWin == true)
		{
			//Win the game
			Win();
		}
		else if (isWin == false)
		{
			//Lose the game
			Lost();
		}
	}

	public void Win()
	{
		//Win the DontGetDrunk game
		Debug.Log("You Win");

		//Stop game
		assignedTarget.GetComponent<MinigameManager>().EndMinigame();
		assignedTarget.GetComponent<DialogueTrigger>().currentDialogue = winDialogue;
		assignedTarget.GetComponent<DialogueTrigger>().TriggerDialogue();
	}

	public void Lost()
	{
		//Lose the DontGetDrunk game
		Debug.Log("You Lose");

		//Stop game
		assignedTarget.GetComponent<MinigameManager>().EndMinigame();
		assignedTarget.GetComponent<DialogueTrigger>().currentDialogue = loseDialogue;
		assignedTarget.GetComponent<DialogueTrigger>().TriggerDialogue();
	}
}
