using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellTrigger : Interactable
{
	public bool isWin;

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
		}
		else if (isWin == false)
		{
			//Lose the game
		}
	}
}
