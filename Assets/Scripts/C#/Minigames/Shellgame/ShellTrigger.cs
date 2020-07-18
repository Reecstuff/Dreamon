using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellTrigger : Interactable
{
	public bool isWin;

	public override void Interact()
	{
		if (GetComponentInParent<ShellgameManager>().animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
		{
			GetComponent<ShellTrigger>().SelectShell();
		}
	}

	public void SelectShell()
	{
		//Check for winthing
		if (isWin == true)
		{
			//Win the game
			GetComponentInParent<ShellgameManager>().Win();
		}
		else if (isWin == false)
		{
			//Lose the game
			GetComponentInParent<ShellgameManager>().Lost();
		}
	}
}
