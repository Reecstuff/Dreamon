using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOManager : MonoBehaviour
{
	public GameObject[] hiddenObjects;
	public int foundObjects;

	public GameObject assignedTarget;
	public int winDialogue;

	private void Update()
	{
		if (hiddenObjects.Length == foundObjects)
		{
			//Stop game
			assignedTarget.GetComponent<MinigameManager>().EndMinigame();
			assignedTarget.GetComponent<DialogueTrigger>().currentDialogue = winDialogue;
			assignedTarget.GetComponent<DialogueTrigger>().TriggerDialogue();
		}
	}
}
