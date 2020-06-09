using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkManager : MonoBehaviour
{
	public GameObject assignedTarget;
	public GameObject bottle;

	public int drinkBottles;
	public int maxBottles;

	public int drunkBottles;
	public int maxAlcBottles;

	public float drinkTime = 3;

	public int winDialogue;
	public int loseDialogue;

	public bool gameFinishied = false;

	private void Update()
	{
		drinkTime -= Time.deltaTime;

		if (gameFinishied == false)
		{
			if (drinkTime >= 0)
			{
				if (drinkBottles == maxBottles)
				{
					Win();
				}
				if (drunkBottles == maxAlcBottles)
				{
					//Drink to much alcohole
					Lost();
				}
			}
			else
			{
				Lost();
			}
		}
	}

	public void Win()
	{
		//Win the DontGetDrunk game
		Debug.Log("You Win");
		gameFinishied = true;
		//Dialogue.Opinion opinion = dialogue.opinion[selectedOpinion];

		//Stop game
		assignedTarget.GetComponent<MinigameManager>().EndMinigame();
		assignedTarget.GetComponent<DialogueTrigger>().currentDialogue = winDialogue;
		assignedTarget.GetComponent<DialogueTrigger>().TriggerDialogue();
	}

	public void Lost()
	{
		//Lose the DontGetDrunk game
		Debug.Log("You Lose");
		gameFinishied = true;
		//Dialogue.Opinion opinion = dialogue.opinion[selectedOpinion];

		//Stop game
		assignedTarget.GetComponent<MinigameManager>().EndMinigame();
		assignedTarget.GetComponent<DialogueTrigger>().currentDialogue = loseDialogue;
		assignedTarget.GetComponent<DialogueTrigger>().TriggerDialogue();
	}
}
