using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOManager : MonoBehaviour
{
	public GameObject assignedTarget;

	public List<GameObject> hiddenObjects;

	public int winDialogue;

	private void Update()
	{
		if (hiddenObjects == null)
		{
			//Win the game

			//Stop game
			assignedTarget.GetComponent<MinigameManager>().EndMinigame();
			assignedTarget.GetComponent<DialogueTrigger>().TriggerDialogue();
			assignedTarget.GetComponent<DialogueTrigger>().currentDialogue = winDialogue;
		}
	}
}
